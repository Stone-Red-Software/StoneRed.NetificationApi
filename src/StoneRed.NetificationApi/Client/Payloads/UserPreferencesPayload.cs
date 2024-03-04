using StoneRed.NetificationApi.Client.Models;

namespace StoneRed.NetificationApi.Client.Payloads;

internal class UserPreferencesPayload
{
    public required List<NotificationUserPreference> UserPreferences { get; set; }
}