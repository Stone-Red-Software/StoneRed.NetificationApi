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

    private readonly WebsocketClient client;

    public NotificationApiClient(string userId, string clientId, string clientSecret, bool secureMode, string baseAddress = "wss://ws.notificationapi.com")
    {
        UriBuilder uriBuilder = new UriBuilder(baseAddress);

        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["envId"] = clientId;
        query["userId"] = userId;

        if (secureMode)
        {
            query["userIdHash"] = UserIdHasher.Hash(userId, clientSecret);
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
            .Select(msg => WebsocketMessageConverter.ConvertWebsocketMessage<NotificationsReceivedPayload>(msg.Text!))
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
           .Select(msg => WebsocketMessageConverter.ConvertWebsocketMessage<NotificationsReceivedPayload>(msg.Text!))
           .Subscribe(msg =>
           {
               if (msg.Payload is null)
               {
                   return;
               }

               NewNotificationsReceived?.Invoke(this, new NotificationsReceivedEventArgs(msg.Payload.Notifications));
           });
    }

    public Task Start()
    {
        return client.StartOrFail();
    }

    public bool RequestNotifications()
    {
        WebsocketMessage<object> message = new("inapp_web/notifications")
        {
            Payload = new
            {
                count = 50
            }
        };

        return client.Send(JsonSerializer.Serialize(message, Configuration.JsonSerializerOptions));
    }

    public Task Stop()
    {
        return client.StopOrFail(WebSocketCloseStatus.NormalClosure, "Normal closure");
    }
}