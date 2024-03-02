using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Server.Send;

internal class NotificationEmailAttachments
{
    public required string FileName { get; set; }

    public required string Url { get; set; }

    [SetsRequiredMembers]
    public NotificationEmailAttachments(string fileName, string url)
    {
        FileName = fileName;
        Url = url;
    }

    public NotificationEmailAttachments()
    {
    }
}