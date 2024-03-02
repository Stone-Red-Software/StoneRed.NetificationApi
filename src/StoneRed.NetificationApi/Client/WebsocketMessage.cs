using System.Diagnostics.CodeAnalysis;

namespace StoneRed.NetificationApi.Client;

internal class WebsocketMessage : IWebsocketMessage
{
    public required string Route { get; set; }

    [SetsRequiredMembers]
    public WebsocketMessage(string route)
    {
        Route = route;
    }

    public WebsocketMessage()
    {
    }
}

internal class WebsocketMessage<T> : IWebsocketMessage
{
    public required string Route { get; set; }
    public T? Payload { get; set; }

    [SetsRequiredMembers]
    public WebsocketMessage(string route)
    {
        Route = route;
    }

    public WebsocketMessage()
    {
    }
}

internal interface IWebsocketMessage
{
    string Route { get; set; }
}