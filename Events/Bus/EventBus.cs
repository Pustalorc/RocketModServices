using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.RocketModServices.Events.Implementations;
using Pustalorc.Libraries.RocketModServices.Events.Interfaces;
using Pustalorc.Libraries.RocketModServices.Events.Interfaces.Generics;

namespace Pustalorc.Libraries.RocketModServices.Events.Bus;

/// <summary>
///     The global event bus for the application. Fully static.
/// </summary>
[PublicAPI]
public static class EventBus
{
    private static Dictionary<Type, object> Events { get; }

    static EventBus()
    {
        Events = new Dictionary<Type, object>();
    }

    private static TEvent GetEvent<TEvent>()
        where TEvent : IEvent, new()
    {
        var type = typeof(TEvent);

        if (Events.ContainsKey(type) && Events[type] is TEvent @event)
            return @event;

        var newEvent = new TEvent();
        Events.Add(type, newEvent);

        return newEvent;
    }

    /// <summary>
    ///     Subscribes to the specified event type with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will subscribe to the event.</param>
    /// <typeparam name="TEvent">The event type to subscribe to.</typeparam>
    public static void Subscribe<TEvent>(object handler)
        where TEvent : IEvent, new()
    {
        var @event = GetEvent<TEvent>();
        @event.Subscribe(handler);
    }

    /// <summary>
    ///     Subscribes to the specified event type with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will subscribe to the event.</param>
    /// <typeparam name="TEvent">The event type to subscribe to.</typeparam>
    public static void Subscribe<TEvent>(Action handler)
        where TEvent : Event, new()
    {
        var @event = GetEvent<TEvent>();
        @event.Subscribe(handler);
    }

    /// <summary>
    ///     Subscribes to the specified event type with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will subscribe to the event.</param>
    /// <typeparam name="TEvent">The event type to subscribe to.</typeparam>
    /// <typeparam name="TEventArgs">The type of the event's arguments passed to the handler on publish.</typeparam>
    public static void Subscribe<TEvent, TEventArgs>(Action<TEventArgs> handler)
        where TEvent : IEvent<TEventArgs>, new()
        where TEventArgs : struct
    {
        var @event = GetEvent<TEvent>();
        @event.Subscribe(handler);
    }

    /// <summary>
    ///     Unsubscribes from the specified event type with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will unsubscribe from the event.</param>
    /// <typeparam name="TEvent">The event type to unsubscribe from.</typeparam>
    public static void Unsubscribe<TEvent>(object handler)
        where TEvent : IEvent, new()
    {
        var @event = GetEvent<TEvent>();
        @event.Unsubscribe(handler);
    }

    /// <summary>
    ///     Unsubscribes from the specified event type with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will unsubscribe from the event.</param>
    /// <typeparam name="TEvent">The event type to unsubscribe from.</typeparam>
    public static void Unsubscribe<TEvent>(Action handler)
        where TEvent : Event, new()
    {
        var @event = GetEvent<TEvent>();
        @event.Unsubscribe(handler);
    }

    /// <summary>
    ///     Unsubscribes from the specified event type with the specified handler.
    /// </summary>
    /// <param name="handler">The method that will unsubscribe from the event.</param>
    /// <typeparam name="TEvent">The event type to unsubscribe from.</typeparam>
    /// <typeparam name="TEventArgs">The type of the event's arguments passed to the handler on publish.</typeparam>
    public static void Unsubscribe<TEvent, TEventArgs>(Action<TEventArgs> handler)
        where TEvent : IEvent<TEventArgs>, new()
        where TEventArgs : struct
    {
        var @event = GetEvent<TEvent>();
        @event.Unsubscribe(handler);
    }

    /// <summary>
    ///     Publishes and invokes any subscribed handlers.
    /// </summary>
    /// <param name="message">The message to pass to the subscribed handlers.</param>
    /// <typeparam name="TEvent">The event type to publish.</typeparam>
    public static void Publish<TEvent>(object? message)
        where TEvent : IEvent, new()
    {
        var @event = GetEvent<TEvent>();
        @event.Publish(message);
    }

    /// <summary>
    ///     Publishes and invokes any subscribed handlers.
    /// </summary>
    /// <typeparam name="TEvent">The event type to publish.</typeparam>
    public static void Publish<TEvent>()
        where TEvent : Event, new()
    {
        var @event = GetEvent<TEvent>();
        @event.Publish();
    }

    /// <summary>
    ///     Publishes and invokes any subscribed handlers.
    /// </summary>
    /// <param name="message">The message to pass to the subscribed handlers.</param>
    /// <typeparam name="TEvent">The event type to publish.</typeparam>
    /// <typeparam name="TEventArgs">The type of the event's arguments passed to the subscribed handlers.</typeparam>
    public static void Publish<TEvent, TEventArgs>(TEventArgs message)
        where TEvent : IEvent<TEventArgs>, new()
        where TEventArgs : struct
    {
        var @event = GetEvent<TEvent>();
        @event.Publish(message);
    }
}