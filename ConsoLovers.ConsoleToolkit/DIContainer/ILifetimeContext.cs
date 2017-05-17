// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILifetimeContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;

   /// <summary>Used to define a contextual lifetime of <see cref="IExtendedContainer"/> components.</summary>
   /// <seealso cref="System.IDisposable"/>
   /// <remarks>
   ///    PLEASE NOTE: registering of components have to be made via the context, resolving via the container. The benefit of the contextual lifetime is: - you can span a context via
   ///    using (var context = LifetimeContextFactory.Create(container)) { } - in this context the components you register in the context will return the same instance when resolved via
   ///    container - if the context is destroyed, all registered components via the context will be removed from the container - if you span a new context, new instances will be
   ///    returned for the registered components (but you have to re-register all components) sample usage:
   ///    <![CDATA[
   /// var container = new Container();
   /// using (var context = LifetimeContextFactory.Create(container))
   /// {
   ///    context.Register<IMyComponent, MyComponent>(); // Registering via context
   ///    var myComponent = container.Resolve<IMyComponent>(); // Resolving via container
   ///    var myComponent2 = container.Resolve<IMyComponent>(); // Will return the same instance as myComponent
   /// 
   ///    context.Register<IMyComponent, MyOtherComponent>().Named("OtherComponent"); // Named component registering is also possible via fluent syntax
   ///    var myOtherComponent = container.ResolveNamed<IMyComponent>("OtherComponent"); // Resolving of named entry via container
   /// }
   /// // Context is now destroyed and all components registered via context are now unregistered from the container
   /// var myComponent3 = container.Resolve<IMyComponent>(); // Will return null because IMyComponent was registered in the context
   /// ]]>
   /// </remarks>
   public interface ILifetimeContext : IDisposable
   {
      #region Public Methods and Operators

      /// <summary>Registers the type at the container.</summary>
      /// <param name="service">The service type to register.</param>
      /// <param name="handler">The the function that constructs the object.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry Register(Type service, Func<IContainer, object> handler);

      /// <summary>Registers the type at the container.</summary>
      /// <typeparam name="T">The service type</typeparam>
      /// <param name="handler">The the function that constructs the object.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry Register<T>(Func<IContainer, object> handler)
         where T : class;

      /// <summary>Registers the instance at the container.</summary>
      /// <param name="service">The service to register. </param>
      /// <param name="implementation">The implementation of the service. </param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry Register(Type service, object implementation);

      /// <summary>Registers the specified service type with the implementation type, that is constructed on request.</summary>
      /// <param name="service">The service type.</param>
      /// <param name="implementation">The implementation type.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry Register(Type service, Type implementation);

      /// <summary>Registers the service type with an implementation type.</summary>
      /// <typeparam name="T">The type of the service to register.</typeparam>
      /// <param name="implementation">The implementation of the service.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry Register<T>(object implementation)
         where T : class;

      /// <summary>Registers this instance.</summary>
      /// <typeparam name="TService">The type of the service to register.</typeparam>
      /// <typeparam name="TImplementation">The type of the implementation for the type.</typeparam>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry Register<TService, TImplementation>()
         where TService : class where TImplementation : class;

      #endregion
   }
}