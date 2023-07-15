using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.RocketModServices.Services.Exceptions;

/// <inheritdoc />
/// <summary>
///     An exception thrown whenever a service is attempted to be fetched but the service has not been registered.
/// </summary>
[PublicAPI]
public sealed class NoServiceRegisteredException<T> : Exception
{
    /// <inheritdoc />
    public NoServiceRegisteredException() : base($"No service is registered for type {typeof(T)}")
    {
    }
}