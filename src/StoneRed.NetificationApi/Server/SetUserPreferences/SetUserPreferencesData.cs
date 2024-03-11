using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.SetUserPreferences;

/// <summary>
/// Represents the data for setting user preferences.
/// </summary>
public class SetUserPreferencesData
{
    /// <summary>
    /// Gets or sets the list of notification preferences.
    /// </summary>
    public required List<NotificationPreference> Preferences { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetUserPreferencesData"/> class.
    /// </summary>
    /// <param name="preferences">The list of notification preferences.</param>
    [SetsRequiredMembers]
    public SetUserPreferencesData(List<NotificationPreference> preferences)
    {
        Preferences = preferences;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetUserPreferencesData"/> class.
    /// </summary>
    public SetUserPreferencesData()
    {
    }
}