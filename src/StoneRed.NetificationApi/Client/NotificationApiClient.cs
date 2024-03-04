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

public class NotificationApiClient
{
    public event EventHandler<NotificationsReceivedEventArgs>? RequestedNotificationsReceived;

    public event EventHandler<NotificationsReceivedEventArgs>? NewNotificationsReceived;

    public event EventHandler<CountReceivedEventArgs>? UnreadCountReceived;

    public event EventHandler<UserPreferencesReceivedEventArgs>? UserPreferencesReceived;

    private readonly WebsocketClient client;

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

    public Task Start()
    {
        return client.StartOrFail();
    }

    public bool RequestNotifications(int count)
    {
        WebsocketMessage<CountPayload> message = new("inapp_web/notifications")
        {
            Payload = new(count)
        };

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    public bool RequestUnreadCount()
    {
        WebsocketMessage message = new("inapp_web/unread_count");

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    public bool ClearUnread()
    {
        WebsocketMessage message = new("inapp_web/unread_clear");

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

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

    public bool RequestUserPreferences()
    {
        WebsocketMessage message = new("user_preferences/get_preferences");

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

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

    public Task Stop()
    {
        return client.StopOrFail(WebSocketCloseStatus.NormalClosure, "Normal closure");
    }
}