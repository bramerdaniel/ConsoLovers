// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContainer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.DIContainer.Strategies;

   /// <summary>Interface for the depencency injection container</summary>
   public interface IContainer
   {
      #region Public Methods and Operators

      /// <summary>Builds up the given object and injects the dependencies.</summary>
      /// <param name="instance">The instance.</param>
      void BuildUp(object instance);

      /// <summary>Builds up the given object and injects the dependencies.</summary>
      /// <param name="instance">The instance.</param>
      /// <param name="selectionStrategy">The selection strategy for the properties to inject.</param>
      void BuildUp(object instance, PropertySelectionStrategy selectionStrategy);

      /// <summary>Creates the specified type using the default options. Tries to resolve the needed dependancies.</summary>
      /// <param name="type">The type to create.</param>
      /// <returns>The created instance</returns>
      object Create(Type type);

      /// <summary>Creates the specified type using the give <see cref="ContainerOptions"/>. Tries to resolve the needed dependancies.</summary>
      /// <param name="type">The type to create.</param>
      /// <param name="options">The used options.</param>
      /// <returns>The created instance</returns>
      object Create(Type type, ContainerOptions options);

      /// <summary>Creates an instance .</summary>
      /// <typeparam name="T">The type of the object to create</typeparam>
      /// <returns>The created instance</returns>
      T Create<T>()
         where T : class;

      /// <summary>Creates the specified type using the give <see cref="ContainerOptions"/>. Tries to resolve the needed dependancies.</summary>
      /// <typeparam name="T">The type of the object to create</typeparam>
      /// <param name="options">The used options.</param>
      /// <returns>The created instance</returns>
      T Create<T>(ContainerOptions options)
         where T : class;

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

      /// <summary>Registers the specified service type with the implementation type, that is contructed on request.</summary>
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

      /// <summary>
      /// Registers the service type with an implementation type.
      /// </summary>
      /// <typeparam name="T">The type of the service to register.</typeparam>
      /// <param name="implementation">The implementation of the service.</param>
      /// <param name="name">The name.</param>
      /// <returns>
      /// The registered container element used for fluent configuration
      /// </returns>
      IContainerEntry RegisterNamed<T>(object implementation, string name)
         where T : class;

      IContainerEntry RegisterNamed(Type service, object implementation, string name);

      /// <summary>Registers the specified service type with the implementation type with the given name.</summary>
      /// <param name="service">The service type.</param>
      /// <param name="implementation">The implementation type.</param>
      /// <param name="named">The named.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry RegisterNamed(Type service, Type implementation, string named);

      /// <summary>Registers the service type with the given name .</summary>
      /// <typeparam name="TService">The type of the service to register.</typeparam>
      /// <typeparam name="TImplementation">The type of the implementation for the type.</typeparam>
      /// <param name="name">The name the dependency should be registered with.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      IContainerEntry RegisterNamed<TService, TImplementation>(string name)
         where TService : class where TImplementation : class;

      /// <summary>Resolves the registered implementation for the given type.</summary>
      /// <param name="service">The service.</param>
      /// <returns>The resolved type</returns>
      object Resolve(Type service);

      /// <summary>Resolves the registered implementation for the given type.</summary>
      /// <typeparam name="T">The service type to resolve</typeparam>
      /// <returns>The resolved type</returns>
      T Resolve<T>()
         where T : class;

      /// <summary>Gets all the instances registered for the given type.</summary>
      /// <param name="service">The service to resolve.</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of the registered imlpementations</returns>
      IEnumerable<object> ResolveAll(Type service);

      /// <summary>Gets all the instances registered for the given type.</summary>
      /// <typeparam name="T">The service to resolve</typeparam>
      /// <returns>An <see cref="IEnumerable{T}"/> of the registered imlpementations</returns>
      IEnumerable<T> ResolveAll<T>()
         where T : class;

      /// <summary>Resolves the registered implementation with the given type for the given key.</summary>
      /// <typeparam name="T">The service type to resolve</typeparam>
      /// <param name="name">The key the implementation was registered with.</param>
      /// <returns>The resolved type</returns>
      T ResolveNamed<T>(string name)
         where T : class;

      /// <summary>Resolves the registered implementation with the given type for the given key.</summary>
      /// <param name="service">The service type to resolve.</param>
      /// <param name="name">The key the implementation was registered with.</param>
      /// <returns>The resolved type</returns>
      object ResolveNamed(Type service, string name);

      #endregion
   }
}