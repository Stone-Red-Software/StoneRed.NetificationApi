namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents the event arguments for notifications received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NotificationsReceivedEventArgs"/> class.
/// </remarks>
/// <param name="notifications">The list of notifications received.</param>
public class NotificationsReceivedEventArgs(List<NotificationReceivedData> notifications) : EventArgs
{
    /// <summary>
    /// Gets or sets the list of notifications received.
    /// </summary>
    public List<NotificationReceivedData> Notifications { get; set; } = notifications;
}