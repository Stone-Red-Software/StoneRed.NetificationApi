using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

public class IdentifyUserData
{
    [JsonIgnore]
    public required string UserId { get; set; }

    public string? Email { get; set; }

    [JsonPropertyName("number")]
    public string? TelephoneNumber { get; set; }

    public List<NotificationPushToken>? PushTokens { get; set; }
    public List<NotificationWebPushToken>? WebPushTokens { get; set; }

    [SetsRequiredMembers]
    public IdentifyUserData(string userId)
    {
        UserId = userId;
    }

    public IdentifyUserData()
    {
    }
}