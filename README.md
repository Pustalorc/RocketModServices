# RocketMod Service Manager [![NuGet](https://img.shields.io/nuget/v/Pustalorc.RocketModServices.svg)](https://www.nuget.org/packages/Pustalorc.RocketModServices/)

Library that adds a BackgroundWorker and TaskQueue with Async support.

# Installation & Usage

Before you begin, please make sure that your current solution has installed the nuget package of this library.  
You can find this package [here.](https://www.nuget.org/packages/Pustalorc.RocketModServices)

---

## Service Usage

The service system is the primary utility included in this library.  
It allows static access to plugin defined services.  

Example usage of registering and retrieving a service named `BuildableAbstractionsService`:

```cs
// Registering the service
RocketModService<IBuildableAbstractionsService>.RegisterService(new BuildableAbstractionsServiceImplementation());

// Retrieving the service (but will throw an exception if its not registered, useful if the service is a hard requirement)
var buildableAbstractionService = RocketModService<IBuildableAbstractionService>.GetService();

// Retrieveing the service (but will return null if its not registered, useful if the service is a soft requirement)
var buildableAbstractionService = RocketModService<IBuildableAbstractionService>.TryGetService();
```
Do note that it is best to target an interface with services, rather than a specific class.  
The clas that stores the services are per-type, so registering the service to the class rather than the interface
will make it impossible to find through the interface unless the other developers target the class when searching for the service.  
With the example above, if the service is registered to `RocketModService<BuildableAbstractionsServiceImplementation>`, 
it will only be accessible through that exact type, and not through `RocketModService<IBuildableAbstractionService>`

## Event Bus Usage

The event bus is the secondary and simple utility included in this library.  
It helps services raise events whilst still allowing services to be replaced without event handlers needing to be resubscribed.  
The usage is simple, if you want to hook to an event, for example `BuildableDestroyedEvent`, simply call `Subscribe`:

```cs
EventBus.Subscribe<BuildableDestroyedEvent>(MyHandlingMethod);

public void MyHandlingMethod(BuildableDestroyedEventArguments eventArguments)
{
}
```

If you wish to unsubscribe from a method, you simply run the same code but call `Unsubscribe`:

```cs
EventBus.Unsubscribe<BuildableDestroyedEvent>(MyHandlingMethod);

public void MyHandlingMethod(BuildableDestroyedEventArguments eventArguments)
{
}
```

If you own the event and you wish to raise a message to all handlers that subscribed to your event, simply call `Publish`:

```cs
EventBus.Publish<BuildableDestroyedEvent>(new BuildableDestroyedEventArguments(buildable));
```