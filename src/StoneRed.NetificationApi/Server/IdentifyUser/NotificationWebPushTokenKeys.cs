using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

public class NotificationWebPushTokenKeys
{
    public required string P256dh { get; set; }
    public required string Auth { get; set; }

    [SetsRequiredMembers]
    public NotificationWebPushTokenKeys(string p256dh, string auth)
    {
        P256dh = p256dh;
        Auth = auth;
    }

    public NotificationWebPushTokenKeys()
    {
    }
}