using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.RocketModServices.Events.Interfaces.Generics;

/// <inheritdoc />
/// <summary>
///     Event interface that exposes subscribe, unsubscribe and publish methods with an event arguments struct.
/// </summary>
/// <typeparam name="TEventArgs">The struct representing the event arguments.</typeparam>
[PublicAPI]
public interface IEvent<TEventArgs> : IEvent where TEventArgs : struct
{
    /// <summary>
    ///     Subscribes to the event with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will subscribe to the event.</param>
    public void Subscribe(Action<TEventArgs> handler);

    /// <summary>
    ///     Unsubscribes from the event with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will unsubscribe from the event.</param>
    public bool Unsubscribe(Action<TEventArgs> handler);

    /// <summary>
    ///     Publishes and invokes any subscribed handlers.
    /// </summary>
    /// <param name="message">The message to pass to the subscribed handlers.</param>
    public void Publish(TEventArgs message);
}