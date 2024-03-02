using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

public class NotificationPushTokenDevice
{
    [JsonPropertyName("app_id")]
    public string? AppId { get; set; }

    [JsonPropertyName("ad_id")]
    public string? AdId { get; set; }

    [JsonPropertyName("device_id")]
    public required string DeviceId { get; set; }

    public string? Platform { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }

    [SetsRequiredMembers]
    public NotificationPushTokenDevice(string deviceId)
    {
        DeviceId = deviceId;
    }

    public NotificationPushTokenDevice()
    {
    }
}