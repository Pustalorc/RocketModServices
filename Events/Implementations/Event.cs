using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.RocketModServices.Events.Interfaces;

namespace Pustalorc.Libraries.RocketModServices.Events.Implementations;

/// <inheritdoc />
/// <summary>
///     Abstract event class that does basic implementation for events.
///     <br />
///     This class does not pass any information to the event handlers.
/// </summary>
[PublicAPI]
public abstract class Event : IEvent
{
    private List<Action> Handlers { get; }

    /// <summary>
    ///     Instantiates the event and creates the list for handlers.
    /// </summary>
    protected Event()
    {
        Handlers = new List<Action>();
    }

    /// <inheritdoc />
    public virtual void Subscribe(object handler)
    {
        if (handler is not Action action)
            throw new NotSupportedException();

        Subscribe(action);
    }

    /// <inheritdoc />
    public virtual bool Unsubscribe(object handler)
    {
        if (handler is not Action action)
            throw new NotSupportedException();

        return Unsubscribe(action);
    }

    /// <inheritdoc />
    public virtual void Publish(object? message)
    {
        Publish();
    }

    /// <summary>
    ///     Subscribes to the event with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will subscribe to the event.</param>
    public virtual void Subscribe(Action handler)
    {
        Handlers.Add(handler);
    }

    /// <summary>
    ///     Unsubscribes from the event with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will unsubscribe from the event.</param>
    public virtual bool Unsubscribe(Action handler)
    {
        return Handlers.Remove(handler);
    }

    /// <summary>
    ///     Publishes and invokes any subscribed handlers.
    /// </summary>
    public virtual void Publish()
    {
        foreach (var handler in Handlers)
            handler.Invoke();
    }
}