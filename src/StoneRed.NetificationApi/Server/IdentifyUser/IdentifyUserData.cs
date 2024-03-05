using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StoneRed.NetificationApi.Server.IdentifyUser;

/// <summary>
/// Represents the data for identifying a user.
/// </summary>
public class IdentifyUserData
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    [JsonIgnore]
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the telephone number of the user.
    /// </summary>
    [JsonPropertyName("number")]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the list of push tokens for the user.
    /// </summary>
    public List<NotificationPushToken>? PushTokens { get; set; }

    /// <summary>
    /// Gets or sets the list of web push tokens for the user.
    /// </summary>
    public List<NotificationWebPushToken>? WebPushTokens { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentifyUserData"/> class with the specified user ID.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    [SetsRequiredMembers]
    public IdentifyUserData(string userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentifyUserData"/> class.
    /// </summary>
    public IdentifyUserData()
    {
    }
}