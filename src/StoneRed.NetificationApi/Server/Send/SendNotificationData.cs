using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.Send;

public class SendNotificationData
{
    public required string NotificationId { get; set; }

    public string? SubNotificationId { get; set; }

    public string? TemplateId { get; set; }

    public required NotificationUser User { get; set; }

    public Dictionary<string, object>? MergeTags { get; set; }
    public Dictionary<string, string>? Replace { get; set; }

    public NotificationOptions? Options { get; set; }

    [SetsRequiredMembers]
    public SendNotificationData(string notificationId, NotificationUser user)
    {
        NotificationId = notificationId;
        User = user;
    }

    public SendNotificationData()
    {
    }
}