using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.RocketModServices.Events.Interfaces.Generics;

namespace Pustalorc.Libraries.RocketModServices.Events.Implementations.Generics;

/// <inheritdoc />
/// <summary>
///     Abstract event class that does basic implementation for events.
/// </summary>
/// <typeparam name="TEventArgs">The struct representing the event arguments.</typeparam>
[PublicAPI]
public abstract class Event<TEventArgs> : IEvent<TEventArgs> where TEventArgs : struct
{
    private List<Action<TEventArgs>> Handlers { get; }

    /// <summary>
    ///     Instantiates the event and creates the list for handlers.
    /// </summary>
    protected Event()
    {
        Handlers = new List<Action<TEventArgs>>();
    }

    /// <inheritdoc />
    public virtual void Subscribe(object handler)
    {
        if (handler is not Action<TEventArgs> action)
            throw new NotSupportedException();

        Subscribe(action);
    }

    /// <inheritdoc />
    public virtual void Subscribe(Action<TEventArgs> handler)
    {
        Handlers.Add(handler);
    }

    /// <inheritdoc />
    public virtual bool Unsubscribe(object handler)
    {
        if (handler is not Action<TEventArgs> action)
            throw new NotSupportedException();

        return Unsubscribe(action);
    }

    /// <inheritdoc />
    public virtual bool Unsubscribe(Action<TEventArgs> handler)
    {
        return Handlers.Remove(handler);
    }

    /// <inheritdoc />
    public virtual void Publish(object? message)
    {
        if (message is not TEventArgs args)
            throw new NotSupportedException();

        Publish(args);
    }

    /// <inheritdoc />
    public virtual void Publish(TEventArgs message)
    {
        foreach (var handler in Handlers)
            handler.Invoke(message);
    }
}