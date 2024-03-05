# StoneRed.NetificationApi

> A .NET library for NotificationAPI

This is an *unofficial* library for [NotificationAPI](https://www.notificationapi.com/) and attempts to replicate the functionality of the official [Server SDK](https://docs.notificationapi.com/reference/server) and [JS Client SDK](https://docs.notificationapi.com/reference/js-client) as closely as possible.\
(A prebuilt notification widget is not included in this library.)

## Supported Features

**Server:**

- Send all types of notifications
- Retract notifications
- Identify user
- Set user preferences

**Client:**

- Receive `In-App` notification
- Get old notifications
- Get unread notification count
- Clear unread notifications
- Get user preferences
- Update user preferences

## Installation

Package Manager

```bash
Install-Package StoneRed.NetificationApi
```

.NET CLI

```bash
dotnet add package StoneRed.NetificationApi
```

## Usage

### Server

```cs
using StoneRed.NetificationApi.Server;
using StoneRed.NetificationApi.Server.Send;

// Initialize NotificationApiServer
NotificationApiServer notificationApiServer = new NotificationApiServer("<ClientId>", "<ClientSecret>", secureMode: false);

// Construct notification
SendNotificationData sendNotificationData = new SendNotificationData
{
    NotificationId = "<NotificationId>",
    User = new NotificationUser
    {
        Id = "<UserId>",
    }
};

// Send notification
await notificationApiServer.Send(sendNotificationData);
```

### Client

```cs
using StoneRed.NetificationApi.Client;
using StoneRed.NetificationApi.Client.Models;

// The client is used to receive notifications
NotificationApiClient notificationApiClient = new NotificationApiClient("<UserId>", "<ClientId>");

// Listen for new notifications
notificationApiClient.NewNotificationsReceived += (sender, args) =>
{
    Console.WriteLine("New notifications received");
    foreach (NotificationReceivedData notificationReceiveData in args.Notifications)
    {
        Console.WriteLine($"New notification received: {notificationReceiveData.Id}");
    }
};

// Listen for unread count
notificationApiClient.UnreadCountReceived += (sender, args) =>
{
    Console.WriteLine($"Unread count received: {args.Count}");
};

// Request unread count
notificationApiClient.RequestUnreadCount();
```

For a more sophisticated example, please check out this [example](https://github.com/Stone-Red-Software/StoneRed.NetificationApi/blob/main/src/StoneRed.NetificationApi.Example/Program.cs).

# Third party licenses
- [Websocket.Client](https://github.com/Marfusios/websocket-client) - [MIT](https://github.com/Marfusios/websocket-client/blob/master/LICENSE)
