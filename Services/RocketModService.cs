using JetBrains.Annotations;
using Pustalorc.Libraries.RocketModServices.Services.Exceptions;
using Pustalorc.Libraries.RocketModServices.Services.Interfaces;

namespace Pustalorc.Libraries.RocketModServices.Services;

/// <summary>
///     Static class storing a specific service.
/// </summary>
/// <typeparam name="T">The type of the service to register or get.</typeparam>
/// <remarks>
///     C# allows static classes with generics, and they have their own dedicated memory depending on the type.
///     Do note that using an interface and searching for a specific class instance will not work.
/// </remarks>
[PublicAPI]
public static class RocketModService<T> where T : class
{
    /// <summary>
    ///     The instance of the service of type <see typeref="T" />
    /// </summary>
    private static T? Service { get; set; }

    /// <summary>
    ///     Registers a new instance of the service of type <see typeref="T" />.
    /// </summary>
    /// <param name="instance">The new instance of the service, replacing the previous instance.</param>
    public static void RegisterService(T instance)
    {
        if (instance is IService newService)
            newService.Load();

        var oldInstance = Service;
        Service = instance;

        if (oldInstance is IService oldService)
            oldService.Unload();
    }

    /// <summary>
    /// Removes the currently registered service instance.
    /// </summary>
    /// <remarks>
    /// This method should generally not be used, unless you are unloading a large service that has multiple dependencies and cannot function without them.
    /// </remarks>
    public static void UnregisterService()
    {
        if (Service is IService service)
            service.Unload();

        Service = null;
    }

    /// <summary>
    ///     Gets the instance of the registered service.
    /// </summary>
    /// <returns>The instance of the service.</returns>
    /// <exception cref="NoServiceRegisteredException{T}">If there is no service, this exception is thrown.</exception>
    public static T GetService()
    {
        if (Service == null)
            throw new NoServiceRegisteredException<T>();

        return Service;
    }

    /// <summary>
    ///     Gets the instance of the registered service.
    /// </summary>
    /// <returns>The instance of the service, or null if no instance was registered.</returns>
    public static T? TryGetService()
    {
        return Service;
    }
}