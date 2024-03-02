namespace StoneRed.NetificationApi.Client.Models;

public class NotificationsReceivedEventArgs(List<NotificationReceivedData> notifications) : EventArgs
{
    public List<NotificationReceivedData> Notifications { get; set; } = notifications;
}