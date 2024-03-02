using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

public class NotificationWebPushToken
{
    public required NotificationWebPushTokenSub Sub { get; set; }

    [SetsRequiredMembers]
    public NotificationWebPushToken(NotificationWebPushTokenSub sub)
    {
        Sub = sub;
    }

    public NotificationWebPushToken()
    {
    }
}