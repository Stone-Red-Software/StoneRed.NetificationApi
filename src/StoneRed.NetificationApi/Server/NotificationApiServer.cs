﻿using StoneRed.NetificationApi.Server.IdentifyUser;
using StoneRed.NetificationApi.Server.Retract;
using StoneRed.NetificationApi.Server.Send;
using StoneRed.NetificationApi.Server.SetUserPreferences;
using StoneRed.NetificationApi.Utilities;

using System.Net.Http.Json;
using System.Text;

namespace StoneRed.NetificationApi.Server;

/// <summary>
/// Represents a server for the Notification API.
/// </summary>
public class NotificationApiServer
{
    private readonly string clientId;
    private readonly string clientSecret;
    private readonly bool secureMode;
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationApiServer"/> class.
    /// </summary>
    /// <param name="clientId">The client ID.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="secureMode">Indicates whether secure mode is enabled.</param>
    /// <param name="baseAddress">The base address of the API.</param>
    public NotificationApiServer(string clientId, string clientSecret, bool secureMode, string baseAddress = "https://api.notificationapi.com") : this(new HttpClient(), clientId, clientSecret, secureMode, baseAddress)
    {
        this.clientId = clientId;
        this.clientSecret = clientSecret;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationApiServer"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="secureMode">Indicates whether secure mode is enabled.</param>
    /// <param name="baseAddress">The base address of the API.</param>
    public NotificationApiServer(HttpClient httpClient, string clientId, string clientSecret, bool secureMode, string baseAddress = "https://api.notificationapi.com")
    {
        string authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));

        httpClient.BaseAddress = new Uri(new Uri(baseAddress), $"{clientId}/");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authToken}");

        this.clientId = clientId;
        this.clientSecret = clientSecret;
        this.secureMode = secureMode;
        this.httpClient = httpClient;
    }

    /// <summary>
    /// Sends a notification.
    /// </summary>
    /// <param name="sendNotificationData">The data for sending the notification.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> Send(SendNotificationData sendNotificationData)
    {
        return await httpClient.PostAsJsonAsync("sender", sendNotificationData, Configuration.JsonSerializerOptions);
    }

    /// <summary>
    /// Retracts a notification.
    /// </summary>
    /// <param name="retractNotificationData">The data for retracting the notification.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> Retract(RetractNotificationData retractNotificationData)
    {
        return await httpClient.PostAsJsonAsync("sender/retract", retractNotificationData, Configuration.JsonSerializerOptions);
    }

    /// <summary>
    /// Identifies a user.
    /// </summary>
    /// <param name="identifyUserData">The data for identifying the user.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> Identify(IdentifyUserData identifyUserData)
    {
        string authToken;

        if (secureMode)
        {
            string hashedUserId = UserIdHasher.Hash(identifyUserData.UserId, clientSecret);
            authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{identifyUserData.UserId}:{hashedUserId}"));
        }
        else
        {
            authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{identifyUserData.UserId}"));
        }

        var requestData = new
        {
            email = identifyUserData.Email,
            number = identifyUserData.TelephoneNumber,
        };

        HttpRequestMessage request = new(HttpMethod.Post, $"users/{identifyUserData.UserId}")
        {
            Content = JsonContent.Create(requestData, options: Configuration.JsonSerializerOptions),
        };

        request.Headers.Add("Authorization", $"Basic {authToken}");

        return await httpClient.SendAsync(request);
    }

    /// <summary>
    /// Sets user preferences.
    /// </summary>
    /// <param name="setUserPreferencesData">The data for setting user preferences.</param>
    /// <returns>The HTTP response message.</returns>
    public async Task<HttpResponseMessage> SetUserPreferences(SetUserPreferencesData setUserPreferencesData)
    {
        return await httpClient.PostAsJsonAsync($"user_preferences/{setUserPreferencesData.UserId}", setUserPreferencesData, Configuration.JsonSerializerOptions);
    }
}