namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents the event arguments for when user preferences are received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserPreferencesReceivedEventArgs"/> class.
/// </remarks>
/// <param name="userPreferences">The list of user preferences.</param>
public class UserPreferencesReceivedEventArgs(List<NotificationUserPreference> userPreferences)
{
    /// <summary>
    /// Gets or sets the list of user preferences.
    /// </summary>
    public List<NotificationUserPreference> UserPreferences { get; set; } = userPreferences;
}