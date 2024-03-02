using StoneRed.NetificationApi.Client;

using System.Text.Json;

namespace StoneRed.NetificationApi.Utilities;

internal static class WebsocketMessageConverter
{
    public static WebsocketMessage<T> ConvertWebsocketMessage<T>(string message)
    {
        return JsonSerializer.Deserialize<WebsocketMessage<T>>(message, Configuration.JsonSerializerOptions) ?? throw new InvalidOperationException("Failed to deserialize websocket message.");
    }
}