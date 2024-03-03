namespace StoneRed.NetificationApi.Client.Payloads;

internal class CountPayload(int count)
{
    public int Count { get; set; } = count;
}