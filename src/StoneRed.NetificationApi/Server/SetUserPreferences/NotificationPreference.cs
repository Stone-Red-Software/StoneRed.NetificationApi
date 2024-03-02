using StoneRed.NetificationApi.Shared;

using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.SetUserPreferences;

public class NotificationPreference
{
    public required string NotificationId { get; set; }

    public required NotificationChannel Channel { get; set; }

    public required bool State { get; set; }

    [SetsRequiredMembers]
    public NotificationPreference(string notificationId, NotificationChannel channel, bool state)
    {
        NotificationId = notificationId;
        Channel = channel;
        State = state;
    }

    public NotificationPreference()
    {
    }
}