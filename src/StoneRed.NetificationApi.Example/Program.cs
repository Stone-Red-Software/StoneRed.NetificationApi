using StoneRed.NetificationApi.Client;
using StoneRed.NetificationApi.Client.Models;
using StoneRed.NetificationApi.Server;
using StoneRed.NetificationApi.Server.IdentifyUser;
using StoneRed.NetificationApi.Server.Send;

// The ID of the user to send the notification to
string userId = "test";

// The ID of the notification to send
string notificationId = "test";

// The client ID and secret to use for the API
string clientId = Environment.GetEnvironmentVariable("NOTIFICATION_API_CLIENT_ID", EnvironmentVariableTarget.User) ?? "<ClientId>";
string clientSecret = Environment.GetEnvironmentVariable("NOTIFICATION_API_SECRET", EnvironmentVariableTarget.User) ?? "<ClientSecret>";

// The client is used to receive notifications
NotificationApiClient notificationApiClient = new NotificationApiClient(userId, clientId);

// The server is used to send notifications
NotificationApiServer notificationApiServer = new NotificationApiServer(clientId, clientSecret, false);

Console.WriteLine("Start client");
await notificationApiClient.Start();
Console.WriteLine("Client started");

notificationApiClient.RequestedNotificationsReceived += (sender, args) =>
{
    Console.WriteLine("Requested notifications received");
    foreach (NotificationReceivedData notificationReceiveData in args.Notifications)
    {
        Console.WriteLine($"Requested notification received: {notificationReceiveData.Id}");
    }
};

notificationApiClient.NewNotificationsReceived += (sender, args) =>
{
    Console.WriteLine("New notifications received");
    foreach (NotificationReceivedData notificationReceiveData in args.Notifications)
    {
        Console.WriteLine($"New notification received: {notificationReceiveData.Id}");
    }
};

notificationApiClient.UnreadCountReceived += (sender, args) =>
{
    Console.WriteLine($"Unread count received: {args.Count}");
};

notificationApiClient.UserPreferencesReceived += (sender, args) =>
{
    Console.WriteLine("User preferences received");
    foreach (NotificationUserPreference notificationPreference in args.UserPreferences)
    {
        Console.WriteLine($"Notification preference received: {notificationPreference.Title}");
        foreach (NotificationUserPreferenceSetting preference in notificationPreference.Settings)
        {
            Console.WriteLine($"Notification preference: {preference.Channel} {preference.ChannelName} {preference.State}");
        }
    }
};

Console.WriteLine("Request user preferences");
notificationApiClient.RequestUserPreferences();
Console.WriteLine("User preferences requested");

Console.WriteLine("Request unread count");
notificationApiClient.RequestUnreadCount();
Console.WriteLine("Unread count requested");

Console.WriteLine("Request notifications");
notificationApiClient.RequestNotifications(5);
Console.WriteLine("Notifications requested");

Console.WriteLine("Identify user");
await notificationApiServer.Identify(new IdentifyUserData
{
    UserId = userId
});
Console.WriteLine("User identified");

Console.WriteLine("Send notification");
SendNotificationData sendNotificationData = new SendNotificationData
{
    NotificationId = notificationId,
    Schedule = DateTime.Now.AddSeconds(10),
    User = new NotificationUser
    {
        Id = userId
    }
};

await notificationApiServer.Send(sendNotificationData);
Console.WriteLine("Notification sent");

Console.ReadLine();
Console.WriteLine("Stop client");
await notificationApiClient.Stop();
Console.WriteLine("Client stopped");