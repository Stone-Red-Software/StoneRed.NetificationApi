namespace StoneRed.NetificationApi.Client.Models;

public class CountReceivedEventArgs(int count) : EventArgs
{
    public int Count { get; } = count;
}