namespace StoneRed.NetificationApi.Client.Models;

public class NotificationReceivedData
{
    public required string Id { get; set; }
    public required bool Seen { get; set; }
    public required string Title { get; set; }
    public required string RedirectURL { get; set; }
    public required string ImageURL { get; set; }
    public required DateTime Date { get; set; }
}