namespace StoneRed.NetificationApi.Server.Send;

public class NotificationEmailOptions
{
    public string[]? ReplyToAddresses { get; set; }

    public string[]? CcAddresses { get; set; }

    public string[]? BccAddresses { get; set; }

    public string[]? Attachments { get; set; }
}