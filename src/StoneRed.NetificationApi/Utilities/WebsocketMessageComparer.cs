using StoneRed.NetificationApi.Client;

using System.Text.Json;

namespace StoneRed.NetificationApi.Utilities;

internal static class WebsocketMessageComparer
{
    public static bool Compare(string? message, string route)
    {
        if (message is null)
        {
            return false;
        }

        WebsocketMessage? websocketMessage = JsonSerializer.Deserialize<WebsocketMessage>(message, Configuration.JsonSerializerOptions);

        return websocketMessage?.Route == route;
    }
}