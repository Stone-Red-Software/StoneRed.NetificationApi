using StoneRed.NetificationApi.Client.Models;

namespace StoneRed.NetificationApi.Client.Payloads;

internal class NotificationsReceivedPayload
{
    public required List<NotificationReceivedData> Notifications { get; set; }
}