namespace StoneRed.NetificationApi.Server.Send;

public class NotificationApnOptions
{
    public int? Expiry { get; set; }

    public int? Priority { get; set; }

    public string? CollapseId { get; set; }

    public string? ThreadId { get; set; }

    public int? Badge { get; set; }

    public string? Sound { get; set; }

    public bool? ContentAvailable { get; set; }
}