namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents a user's preference for a notification.
/// </summary>
public class NotificationUserPreference
{
    /// <summary>
    /// Gets or sets the notification ID.
    /// </summary>
    public required string NotificationId { get; set; }

    /// <summary>
    /// Gets or sets the title of the notification.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the list of settings for the notification.
    /// </summary>
    public required List<NotificationUserPreferenceSetting> Settings { get; set; }

    /// <summary>
    /// Gets or sets the list of sub-notification preferences.
    /// </summary>
    public required List<object> SubNotificationPreferences { get; set; }
}