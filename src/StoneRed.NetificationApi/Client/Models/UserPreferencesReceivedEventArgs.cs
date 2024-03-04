namespace StoneRed.NetificationApi.Client.Models;

public class UserPreferencesReceivedEventArgs(List<NotificationUserPreference> userPreferences)
{
    public List<NotificationUserPreference> UserPreferences { get; set; } = userPreferences;
}