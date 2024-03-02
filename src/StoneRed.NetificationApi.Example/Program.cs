using StoneRed.NetificationApi.Client;
using StoneRed.NetificationApi.Client.Models;
using StoneRed.NetificationApi.Server;
using StoneRed.NetificationApi.Server.IdentifyUser;
using StoneRed.NetificationApi.Server.Send;

const string userId = "<UserId>";
const string notificationId = "<NotifcationId>";
const string clientId = "<ClientId>";
const string clientSecret = "<ClientSecret>";

Console.WriteLine("Start client");
NotificationApiClient notificationApiClient = new NotificationApiClient("test", clientId, clientSecret, false);
await notificationApiClient.Start();
Console.WriteLine("Client started");

notificationApiClient.RequestNotifications();

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

NotificationApiServer notificationApiServer = new NotificationApiServer(clientId, clientSecret, false);

Console.WriteLine("Identify user");
await notificationApiServer.Identify(new IdentifyUserData
{
    UserId = userId,
    Email = "kek123@stone-red.net"
});
Console.WriteLine("User identified");

Console.WriteLine("Send notification");
SendNotificationData sendNotificationData = new SendNotificationData
{
    NotificationId = notificationId,
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