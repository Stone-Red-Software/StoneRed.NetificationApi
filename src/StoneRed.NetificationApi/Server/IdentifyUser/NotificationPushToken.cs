using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

public class NotificationPushToken
{
    public required NotificationPushProviders Type { get; set; }
    public required string Token { get; set; }
    public required NotificationPushTokenDevice Device { get; set; }

    [SetsRequiredMembers]
    public NotificationPushToken(NotificationPushProviders type, string token, NotificationPushTokenDevice device)
    {
        Type = type;
        Token = token;
        Device = device;
    }

    public NotificationPushToken()
    {
    }
}