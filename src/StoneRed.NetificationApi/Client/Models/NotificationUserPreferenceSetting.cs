using StoneRed.NetificationApi.Shared;

namespace StoneRed.NetificationApi.Client.Models;

public class NotificationUserPreferenceSetting
{
    public required NotificationChannel Channel { get; set; }
    public bool State { get; set; }
    public required string ChannelName { get; set; }
}