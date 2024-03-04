using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StoneRed.NetificationApi.Server.SetUserPreferences;

/// <summary>
/// Represents the data for setting user preferences.
/// </summary>
public class SetUserPreferencesData
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    [JsonIgnore]
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of notification preferences.
    /// </summary>
    public required List<NotificationPreference> Preferences { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetUserPreferencesData"/> class.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="preferences">The list of notification preferences.</param>
    [SetsRequiredMembers]
    public SetUserPreferencesData(string userId, List<NotificationPreference> preferences)
    {
        UserId = userId;
        Preferences = preferences;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetUserPreferencesData"/> class.
    /// </summary>
    public SetUserPreferencesData()
    {
    }
}