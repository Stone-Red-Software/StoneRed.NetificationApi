namespace StoneRed.NetificationApi.Server.Send;

public class NotificationOptions
{
    public NotificationEmailOptions? Email { get; set; }

    public NotificationApnOptions? Apn { get; set; }
}