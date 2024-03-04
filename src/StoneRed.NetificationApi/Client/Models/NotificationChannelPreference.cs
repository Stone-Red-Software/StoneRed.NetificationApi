using StoneRed.NetificationApi.Shared;

using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents a preference for a notification channel.
/// </summary>
public class NotificationChannelPreference
{
    /// <summary>
    /// Gets or sets the channel to set the preference for.
    /// </summary>
    public NotificationChannel Channel { get; set; }

    /// <summary>
    /// Gets or sets the state of the preference.
    /// </summary>
    public bool State { get; set; }

    /// <summary>
    /// Gets or sets the sub notification id.
    /// </summary>
    public string? SubNotificationId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationChannelPreference"/> class.
    /// </summary>
    /// <param name="channel">The channel to set the preference for.</param>
    /// <param name="state">The state of the preference.</param>
    /// <param name="subNotificationId">The sub notification id.</param>
    [SetsRequiredMembers]
    public NotificationChannelPreference(NotificationChannel channel, bool state, string? subNotificationId = null)
    {
        Channel = channel;
        State = state;
        SubNotificationId = subNotificationId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationChannelPreference"/> class.
    /// </summary>
    public NotificationChannelPreference()
    {
    }
}