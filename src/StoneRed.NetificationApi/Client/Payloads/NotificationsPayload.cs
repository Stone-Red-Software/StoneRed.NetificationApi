using StoneRed.NetificationApi.Client.Models;

namespace StoneRed.NetificationApi.Client.Payloads;

internal class NotificationsPayload
{
    public required List<NotificationReceivedData> Notifications { get; set; }
}