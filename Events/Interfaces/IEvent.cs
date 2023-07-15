using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.RocketModServices.Events.Interfaces;

/// <summary>
///     Event interface that exposes subscribe, unsubscribe and publish methods.
/// </summary>
[PublicAPI]
public interface IEvent
{
    /// <summary>
    ///     Subscribes to the event with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will subscribe to the event.</param>
    /// <remarks>
    ///     Any class implementing this method should check that the handler is an <see cref="Action" /> or
    ///     <see cref="Action{T}" />
    /// </remarks>
    public void Subscribe(object handler);

    /// <summary>
    ///     Unsubscribes from the event with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will unsubscribe from the event.</param>
    /// <remarks>
    ///     Any class implementing this method should check that the handler is an <see cref="Action" /> or
    ///     <see cref="Action{T}" />
    /// </remarks>
    public bool Unsubscribe(object handler);

    /// <summary>
    ///     Publishes and invokes any subscribed handlers.
    /// </summary>
    /// <param name="message">The message to pass to the subscribed handlers.</param>
    /// <remarks>
    ///     If the event class does not support a message, the input to this method should be passed as null, and ignored by
    ///     the implementing class.
    /// </remarks>
    public void Publish(object? message);
}