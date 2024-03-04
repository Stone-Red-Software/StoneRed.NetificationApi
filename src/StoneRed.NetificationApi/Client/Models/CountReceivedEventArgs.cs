namespace StoneRed.NetificationApi.Client.Models;

/// <summary>
/// Represents the event arguments for when the count of unread notifications is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CountReceivedEventArgs"/> class with the specified count.
/// </remarks>
/// <param name="count">The count of unread notifications.</param>
public class CountReceivedEventArgs(int count) : EventArgs
{
    /// <summary>
    /// Gets the count of unread notifications.
    /// </summary>
    public int Count { get; } = count;
}