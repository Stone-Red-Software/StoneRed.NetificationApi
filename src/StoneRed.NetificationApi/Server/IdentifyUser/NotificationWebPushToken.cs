using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

/// <summary>
/// Represents a notification web push token.
/// </summary>
public class NotificationWebPushToken
{
    /// <summary>
    /// Gets or sets the sub property of the notification web push token.
    /// </summary>
    public required NotificationWebPushTokenSub Sub { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationWebPushToken"/> class with the specified sub.
    /// </summary>
    /// <param name="sub">The sub value.</param>
    [SetsRequiredMembers]
    public NotificationWebPushToken(NotificationWebPushTokenSub sub)
    {
        Sub = sub;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationWebPushToken"/> class.
    /// </summary>
    public NotificationWebPushToken()
    {
    }
}