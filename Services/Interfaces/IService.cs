using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.RocketModServices.Services.Interfaces;

/// <summary>
/// An interface to define the basics of a service. Primarily to be used by <see cref="RocketModService{T}"/>
/// </summary>
[PublicAPI]
public interface IService
{
    /// <summary>
    /// A method to be called as soon as the service is registered.
    /// </summary>
    public void Load();

    /// <summary>
    /// A method to be called as soon as the service is unregistered.
    /// </summary>
    /// <remarks>
    /// Highly discouraged to implement this method and re-register to your <see cref="RocketModService{T}"/> if you are told to unload.
    /// If 2 service instances were to do this, they would consistently replace each other until a <see cref="StackOverflowException"/> is thrown.
    /// So do not do it.
    /// </remarks>
    public void Unload();
}