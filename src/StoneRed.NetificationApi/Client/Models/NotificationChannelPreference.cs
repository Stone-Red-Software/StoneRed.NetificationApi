using StoneRed.NetificationApi.Shared;

using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Client.Models;

public class NotificationChannelPreference
{
    public NotificationChannel Channel { get; set; }

    public bool State { get; set; }
    public string? SubNotificationId { get; set; }

    [SetsRequiredMembers]
    public NotificationChannelPreference(NotificationChannel channel, bool state, string? subNotificationId = null)
    {
        Channel = channel;
        State = state;
        SubNotificationId = subNotificationId;
    }

    public NotificationChannelPreference()
    {
    }
}