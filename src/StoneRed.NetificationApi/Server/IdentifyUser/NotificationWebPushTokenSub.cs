using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

public class NotificationWebPushTokenSub
{
    public required string Endpoint { get; set; }
    public required NotificationWebPushTokenKeys Keys { get; set; }

    [SetsRequiredMembers]
    public NotificationWebPushTokenSub(string endpoint, NotificationWebPushTokenKeys keys)
    {
        Endpoint = endpoint;
        Keys = keys;
    }

    public NotificationWebPushTokenSub()
    {
    }
}