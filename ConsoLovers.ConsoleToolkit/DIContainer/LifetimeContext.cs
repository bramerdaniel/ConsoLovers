// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LifetimeContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;
   using System.Collections.Generic;

   /// <summary>Used to define a contextual lifetime for <see cref="IExtendedContainer"/> components.</summary>
   /// <seealso cref="ILifetimeContext"/>
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
   public class LifetimeContext : ILifetimeContext
   {
      #region Constants and Fields

      private readonly List<IContainerEntry> containerEntries;

      private readonly IExtendedContainer extendedContainer;

      /// <summary>Internal variable which checks if Dispose has already been called</summary>
      private bool disposed;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="LifetimeContext"/> class.</summary>
      /// <param name="extendedContainer">The <see cref="IExtendedContainer"/>.</param>
      public LifetimeContext(IExtendedContainer extendedContainer)
      {
         this.extendedContainer = extendedContainer;
         containerEntries = new List<IContainerEntry>();
      }

      #endregion

      #region IDisposable Members

      /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
      public void Dispose()
      {
         // Call the private Dispose(bool) helper and indicate 
         // that we are explicitly disposing
         this.Dispose(true);

         // Tell the garbage collector that the object doesn't require any
         // cleanup when collected since Dispose was called explicitly.
         GC.SuppressFinalize(this);
      }

      #endregion

      #region ILifetimeContext Members

      /// <summary>Registers the instance at the container.</summary>
      /// <param name="service">The service to register. </param>
      /// <param name="implementation">The implementation of the service. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register(Type service, object implementation)
      {
         CheckDisposed();
         return Register(service, c => implementation);
      }

      /// <summary>Registers the service type with an implementation type.</summary>
      /// <typeparam name="T">The type of the service to register. </typeparam>
      /// <param name="implementation">The implementation of the service. </param>
      /// ///
      /// <returns> The registered container element used for fluent configuration </returns>
      public IContainerEntry Register<T>(object implementation)
         where T : class
      {
         CheckDisposed();
         var entry = extendedContainer.Register<T>(implementation);
         HandleEntry(entry);

         return entry;
      }

      /// <summary>Registers this instance.</summary>
      /// <typeparam name="TSer">The type to register. </typeparam>
      /// <typeparam name="TImpl">The type of the instance. </typeparam>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register<TSer, TImpl>()
         where TSer : class where TImpl : class
      {
         CheckDisposed();
         return Register(typeof(TSer), typeof(TImpl));
      }

      /// <summary>Registers the type at the container.</summary>
      /// <param name="service">The service type to register. </param>
      /// <param name="handler">The the function that constructs the object. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register(Type service, Func<IContainer, object> handler)
      {
         CheckDisposed();
         var entry = extendedContainer.Register(service, handler);
         HandleEntry(entry);

         return entry;
      }

      /// <summary>Registers the type at the container.</summary>
      /// <typeparam name="T">The service type </typeparam>
      /// <param name="handler">The the function that constructs the object. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register<T>(Func<IContainer, object> handler)
         where T : class
      {
         CheckDisposed();
         return Register(typeof(T), handler);
      }

      /// <summary>Registers the specified service.</summary>
      /// <param name="service">The service. </param>
      /// <param name="implementation">The implementation. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register(Type service, Type implementation)
      {
         CheckDisposed();
         var entry = extendedContainer.Register(service, implementation);
         HandleEntry(entry);

         return entry;
      }

      #endregion

      #region Methods

      private void CheckDisposed()
      {
         if (disposed)
         {
            throw new ObjectDisposedException("LifetimeContext", "LifetimeContext is already disposed.");
         }
      }

      /// <summary>Releases unmanaged and - optionally - managed resources</summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      private void Dispose(bool disposing)
      {
         if (disposed)
         {
            return;
         }

         if (disposing)
         {
            containerEntries.ForEach(ce => extendedContainer.Unregister(ce));
            containerEntries.Clear();
         }

         disposed = true;
      }

      private void HandleEntry(IContainerEntry entry)
      {
         entry.WithLifetime(Lifetime.Singleton);
         containerEntries.Add(entry);
      }

      #endregion
   }
}