namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents the data of a notification received.
/// </summary>
public class NotificationReceivedData
{
    /// <summary>
    /// Gets or sets the ID of the notification.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the notification has been seen.
    /// </summary>
    public required bool Seen { get; set; }

    /// <summary>
    /// Gets or sets the title of the notification.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the redirect URL of the notification.
    /// </summary>
    public required string RedirectURL { get; set; }

    /// <summary>
    /// Gets or sets the image URL of the notification.
    /// </summary>
    public required string ImageURL { get; set; }

    /// <summary>
    /// Gets or sets the date of the notification.
    /// </summary>
    public required DateTime Date { get; set; }
}