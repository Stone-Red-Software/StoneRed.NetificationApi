using StoneRed.NetificationApi.Shared;

namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents a user preference setting for a notification channel.
/// </summary>
public class NotificationUserPreferenceSetting
{
    /// <summary>
    /// Gets or sets the notification channel.
    /// </summary>
    public required NotificationChannel Channel { get; set; }

    /// <summary>
    /// Gets or sets the state of the user preference.
    /// </summary>
    public bool State { get; set; }

    /// <summary>
    /// Gets or sets the name of the notification channel.
    /// </summary>
    public required string ChannelName { get; set; }
}