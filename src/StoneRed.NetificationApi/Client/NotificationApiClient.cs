using StoneRed.NetificationApi.Client.Models;
using StoneRed.NetificationApi.Client.Payloads;
using StoneRed.NetificationApi.Utilities;

using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text.Json;
using System.Web;

using Websocket.Client;

namespace StoneRed.NetificationApi.Client;

/// <summary>
/// Represents a client for interacting with the Notification API.
/// </summary>
public class NotificationApiClient
{
    /// <summary>
    /// Event that is raised when requested notifications are received.
    /// </summary>
    public event EventHandler<NotificationsReceivedEventArgs>? RequestedNotificationsReceived;

    /// <summary>
    /// Event that is raised when new notifications are received.
    /// </summary>
    public event EventHandler<NotificationsReceivedEventArgs>? NewNotificationsReceived;

    /// <summary>
    /// Event that is raised when the unread count is received.
    /// </summary>
    public event EventHandler<CountReceivedEventArgs>? UnreadCountReceived;

    /// <summary>
    /// Event that is raised when user preferences are received.
    /// </summary>
    public event EventHandler<UserPreferencesReceivedEventArgs>? UserPreferencesReceived;

    private readonly WebsocketClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationApiClient"/> class.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="userIdHash">The user ID hash.</param>
    /// <param name="baseAddress">The base address of the WebSocket server.</param>
    public NotificationApiClient(string userId, string clientId, string? userIdHash = null, string baseAddress = "wss://ws.notificationapi.com")
    {
        UriBuilder uriBuilder = new UriBuilder(baseAddress);

        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["envId"] = clientId;
        query["userId"] = userId;

        if (userIdHash is not null)
        {
            query["userIdHash"] = userIdHash;
        }

        uriBuilder.Query = query.ToString();

        client = new WebsocketClient(uriBuilder.Uri);

        _ = client.MessageReceived.Subscribe(msg =>
        {
            Debug.WriteLine("Message:" + msg.Text);
        });

        _ = client.MessageReceived
            .Where(msg => msg.Text is not null)
            .Where(msg => WebsocketMessageComparer.Compare(msg.Text, "inapp_web/notifications"))
            .Select(msg => WebsocketMessageConverter.ConvertWebsocketMessage<NotificationsPayload>(msg.Text!))
            .Subscribe(msg =>
            {
                if (msg.Payload is null)
                {
                    return;
                }

                RequestedNotificationsReceived?.Invoke(this, new NotificationsReceivedEventArgs(msg.Payload.Notifications));
            });

        _ = client.MessageReceived
           .Where(msg => msg.Text is not null)
           .Where(msg => WebsocketMessageComparer.Compare(msg.Text, "inapp_web/new_notifications"))
           .Select(msg => WebsocketMessageConverter.ConvertWebsocketMessage<NotificationsPayload>(msg.Text!))
           .Subscribe(msg =>
           {
               if (msg.Payload is null)
               {
                   return;
               }

               NewNotificationsReceived?.Invoke(this, new NotificationsReceivedEventArgs(msg.Payload.Notifications));
           });

        _ = client.MessageReceived
            .Where(msg => msg.Text is not null)
            .Where(msg => WebsocketMessageComparer.Compare(msg.Text, "inapp_web/unread_count"))
            .Select(msg => WebsocketMessageConverter.ConvertWebsocketMessage<CountPayload>(msg.Text!))
            .Subscribe(msg =>
            {
                if (msg.Payload is null)
                {
                    return;
                }

                UnreadCountReceived?.Invoke(this, new CountReceivedEventArgs(msg.Payload.Count));
            });

        _ = client.MessageReceived
            .Where(msg => msg.Text is not null)
            .Where(msg => WebsocketMessageComparer.Compare(msg.Text, "user_preferences/preferences"))
            .Select(msg => WebsocketMessageConverter.ConvertWebsocketMessage<UserPreferencesPayload>(msg.Text!))
            .Subscribe(msg =>
            {
                if (msg.Payload is null)
                {
                    return;
                }

                UserPreferencesReceived?.Invoke(this, new UserPreferencesReceivedEventArgs(msg.Payload.UserPreferences));
            });
    }

    /// <summary>
    /// Starts the Websocket client.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task Start()
    {
        return client.StartOrFail();
    }

    /// <summary>
    /// Requests notifications from the server.
    /// </summary>
    /// <param name="count">The number of notifications to request.</param>
    /// <returns>True if the request was sent successfully; otherwise, false.</returns>
    public bool RequestNotifications(int count)
    {
        WebsocketMessage<CountPayload> message = new("inapp_web/notifications")
        {
            Payload = new(count)
        };

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    /// <summary>
    /// Requests the unread count from the server.
    /// </summary>
    /// <returns>True if the request was sent successfully; otherwise, false.</returns>
    public bool RequestUnreadCount()
    {
        WebsocketMessage message = new("inapp_web/unread_count");

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    /// <summary>
    /// Clears the unread count on the server.
    /// </summary>
    /// <returns>True if the request was sent successfully; otherwise, false.</returns>
    public bool ClearUnread()
    {
        WebsocketMessage message = new("inapp_web/unread_clear");

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    /// <summary>
    /// Clears the unread count for a specific notification on the server.
    /// </summary>
    /// <param name="notificationId">The ID of the notification to clear.</param>
    /// <returns>True if the request was sent successfully; otherwise, false.</returns>
    public bool ClearUnread(string notificationId)
    {
        WebsocketMessage<object> message = new("inapp_web/unread_clear")
        {
            Payload = new
            {
                notificationId
            }
        };

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    /// <summary>
    /// Requests the user preferences from the server.
    /// </summary>
    /// <returns>True if the request was sent successfully; otherwise, false.</returns>
    public bool RequestUserPreferences()
    {
        WebsocketMessage message = new("user_preferences/get_preferences");

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    /// <summary>
    /// Patches the user preferences for a specific notification on the server.
    /// </summary>
    /// <param name="notificationId">The ID of the notification to patch.</param>
    /// <param name="channelPreferences">The channel preferences to patch.</param>
    /// <returns>True if the request was sent successfully; otherwise, false.</returns>
    public bool PatchUserPreferences(string notificationId, params NotificationChannelPreference[] channelPreferences)
    {
        WebsocketMessage<object> message = new("user_preferences/patch_preferences")
        {
            Payload = new object[]
            {
                    new
                    {
                        notificationId,
                        channelPreferences
                    }
            }
        };

        string msg = JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions);

        return client.Send(msg);
    }

    /// <summary>
    /// Stops the Websocket client.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task Stop()
    {
        return client.StopOrFail(WebSocketCloseStatus.NormalClosure, "Normal closure");
    }
}