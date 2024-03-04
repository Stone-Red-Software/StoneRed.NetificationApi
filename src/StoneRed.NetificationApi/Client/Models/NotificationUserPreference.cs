namespace StoneRed.NetificationApi.Client.Models;

public class NotificationUserPreference
{
    public required string NotificationId { get; set; }
    public required string Title { get; set; }
    public required List<NotificationUserPreferenceSetting> Settings { get; set; }
    public required List<object> SubNotificationPreferences { get; set; }
}