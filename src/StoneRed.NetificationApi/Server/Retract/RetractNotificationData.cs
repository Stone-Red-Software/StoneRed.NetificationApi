using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.Retract;

public class RetractNotificationData
{
    public required string UserId { get; set; }

    public required string NotificationId { get; set; }

    public string? SecondaryId { get; set; }

    [SetsRequiredMembers]
    public RetractNotificationData(string userId, string notificationId)
    {
        UserId = userId;
        NotificationId = notificationId;
    }

    public RetractNotificationData()
    {
    }
}