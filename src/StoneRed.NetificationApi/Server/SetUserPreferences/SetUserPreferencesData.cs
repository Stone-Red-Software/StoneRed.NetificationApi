using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StoneRed.NetificationApi.Server.SetUserPreferences;

public class SetUserPreferencesData
{
    [JsonIgnore]
    public required string UserId { get; set; }

    public required List<NotificationPreference> Preferences { get; set; }

    [SetsRequiredMembers]
    public SetUserPreferencesData(string userId, List<NotificationPreference> preferences)
    {
        UserId = userId;
        Preferences = preferences;
    }

    public SetUserPreferencesData()
    {
    }
}