using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StoneRed.NetificationApi.Server.Send;

public class NotificationUser
{
    public required string Id { get; set; }

    public string? Email { get; set; }

    [JsonPropertyName("number")]
    public string? TelephoneNumber { get; set; }

    public NotificationUser()
    {
    }

    [SetsRequiredMembers]
    public NotificationUser(string id)
    {
        Id = id;
    }
}