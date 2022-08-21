// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Container.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
   #region

   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Runtime.InteropServices;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   #endregion

   /// <summary>Simple implementation of a dependency injection container</summary>
   public class Container : IContainer, IServiceProvider
   {
      // ReSharper disable ExceptionNotDocumented

      #region Constants and Fields

      private readonly List<ServiceDescriptor> entries = new();

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="Container"/> class.</summary>
      public Container()
      {
         Register(typeof(IContainer), this);
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the options the <see cref="Container"/> uses.</summary>
      public ContainerOptions Options { get; set; } = new ContainerOptions();

      #endregion

      #region Public Methods and Operators

      public void Register([NotNull] ServiceDescriptor descriptor)
      {
         if (descriptor == null)
            throw new ArgumentNullException(nameof(descriptor));
         entries.Add(descriptor);
      }

      /// <summary>Builds up the given object and injects the dependencies.</summary>
      /// <param name="instance">The instance. </param>
      /// <exception cref="ArgumentNullException">instance is <see langword="null"/>.</exception>
      /// <exception cref="TargetException">The object does not match the target type.-or-The property is an instance property, but obj /> is null. </exception>
      /// <exception cref="TargetParameterCountException">The number of parameters in index does not match the number of parameters the indexed property takes. </exception>
      /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
      /// <exception cref="TargetInvocationException">
      ///    An error occurred while setting the property value. For example, an index value specified for an indexed
      ///    property is out of range. The <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.
      /// </exception>
      public void BuildUp(object instance)
      {
         BuildUp(instance, Options.PropertySelectionStrategy);
      }

      /// <summary>Builds up the given object and injects the dependencies.</summary>
      /// <param name="instance">The instance. </param>
      /// <param name="strategy">The strategy that selects the properties that are injected. </param>
      /// <exception cref="ArgumentNullException"><paramref name="instance"/> is <see langword="null"/>.</exception>
      /// <exception cref="TargetException">The object does not match the target type.-or-The property is an instance property, but obj is null. </exception>
      /// <exception cref="TargetParameterCountException">The number of parameters in index does not match the number of parameters the indexed property takes. </exception>
      /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
      /// <exception cref="TargetInvocationException">
      ///    An error occurred while setting the property value. For example, an index value specified for an indexed
      ///    property is out of range. The <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.
      /// </exception>
      public void BuildUp([NotNull] object instance, PropertySelectionStrategy strategy)
      {
         if (instance == null)
            throw new ArgumentNullException(nameof(instance), "Container can not build up a null object");

         IEnumerable<PropertyInfo> injectables = strategy.SelectProperties(instance.GetType());
         foreach (var propertyInfo in injectables.ToList())
         {
            var injectionInstance = Resolve(propertyInfo.PropertyType);
            if (injectionInstance != null)
            {
               propertyInfo.SetValue(instance, injectionInstance, null);
            }
         }
      }

      /// <summary>Creates the specified type.</summary>
      /// <param name="type">The type. </param>
      /// <returns>The created instance </returns>
      /// <exception cref="NotSupportedException">
      ///    <paramref name="type"/> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder"/>.-or- Creation of
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and
      ///    <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of those types, is not supported.
      /// </exception>
      /// <exception cref="ArgumentException">
      ///    <paramref name="type"/> is not a RuntimeType. -or-<paramref name="type"/> is an open generic type (that is, the
      ///    <see cref="P:System.Type.ContainsGenericParameters"/> property returns true).
      /// </exception>
      /// <exception cref="ArgumentNullException"><paramref name="type"/> is null. </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type. </exception>
      /// <exception cref="COMException">
      ///    <paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified
      ///    class is not registered.
      /// </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="InvalidComObjectException">
      ///    The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or
      ///    "Overload:System.Type.GetTypeFromCLSID".
      /// </exception>
      /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
      /// <exception cref="MethodAccessException">The caller does not have permission to call this constructor. </exception>
      public object Create(Type type)
      {
         return BuildInstance(type, Options.ConstructorSelectionStrategy);
      }

      /// <summary>Creates the specified type using the give <see cref="ContainerOptions"/> . Tries to resolve the needed dependencies.</summary>
      /// <param name="type">The type to create. </param>
      /// <param name="containerOptions">The used options. </param>
      /// <returns>The created instance </returns>
      /// <exception cref="NotSupportedException">
      ///    <paramref name="type"/> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder"/>.-or- Creation of
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and
      ///    <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of those types, is not supported.
      /// </exception>
      /// <exception cref="ArgumentException">
      ///    <paramref name="type"/> is not a RuntimeType. -or-<paramref name="type"/> is an open generic type (that is, the
      ///    <see cref="P:System.Type.ContainsGenericParameters"/> property returns true).
      /// </exception>
      /// <exception cref="ArgumentNullException"><paramref name="type"/> is null. </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type. </exception>
      /// <exception cref="COMException">
      ///    <paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified
      ///    class is not registered.
      /// </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="InvalidComObjectException">
      ///    The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or
      ///    "Overload:System.Type.GetTypeFromCLSID".
      /// </exception>
      /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
      /// <exception cref="MethodAccessException">The caller does not have permission to call this constructor. </exception>
      public object Create(Type type, ContainerOptions containerOptions)
      {
         return BuildInstance(type, containerOptions.ConstructorSelectionStrategy);
      }

      /// <summary>Creates an instance .</summary>
      /// <typeparam name="T">The type of the object to create </typeparam>
      /// <returns>The created instance </returns>
      public T Create<T>()
         where T : class
      {
         return Create(typeof(T)) as T;
      }

      /// <summary>Creates the specified type using the give <see cref="ContainerOptions"/> . Tries to resolve the needed dependencies.</summary>
      /// <typeparam name="T">The type of the object to create </typeparam>
      /// <param name="buildingOptions">The used options. </param>
      /// <returns>The created instance </returns>
      public T Create<T>(ContainerOptions buildingOptions)
         where T : class
      {
         return Create(typeof(T), buildingOptions) as T;
      }

      public void Register<T>(Func<IServiceProvider, object> handler, ServiceLifetime lifetime)
         where T : class
      {
         Register(typeof(T), handler, lifetime);
      }

      public void Register(Type service, Func<IServiceProvider, object> handler, ServiceLifetime lifetime)
      {
         Register(ServiceDescriptor.Describe(service, handler, lifetime));
      }

      /// <summary>Registers the instance at the container.</summary>
      /// <param name="service">The service to register. </param>
      /// <param name="implementation">The implementation of the service. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public void Register(Type service, object implementation)
      {
         Register(ServiceDescriptor.Singleton(service, implementation));
      }

      /// <summary>Registers the service type with an implementation type.</summary>
      /// <typeparam name="T">The type of the service to register. </typeparam>
      /// <param name="implementation">The implementation of the service. </param>
      /// ///
      /// <returns> The registered container element used for fluent configuration </returns>
      public void Register<T>(object implementation)
         where T : class
      {
         Register(typeof(T), implementation);
      }

      /// <summary>Registers this instance.</summary>
      /// <typeparam name="TSer">The type to register. </typeparam>
      /// <typeparam name="TImpl">The type of the instance. </typeparam>
      /// <returns>The registered container element used for fluent configuration </returns>
      public void Register<TSer, TImpl>()
         where TSer : class where TImpl : class
      {
         Register(typeof(TSer), typeof(TImpl));
      }

      /// <summary>Registers the type at the container.</summary>
      /// <param name="service">The service type to register. </param>
      /// <param name="handler">The the function that constructs the object. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public void Register(Type service, Func<IServiceProvider, object> handler)
      {
         Register(ServiceDescriptor.Scoped(service, handler));
      }

      /// <summary>Registers the type at the container.</summary>
      /// <typeparam name="T">The service type </typeparam>
      /// <param name="handler">The the function that constructs the object. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public void Register<T>(Func<IServiceProvider, object> handler)
         where T : class
      {
         Register(typeof(T), handler);
      }

      /// <summary>Registers the specified service.</summary>
      /// <param name="service">The service. </param>
      /// <param name="implementation">The implementation. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public void Register(Type service, Type implementation)
      {
         Register(ServiceDescriptor.Scoped(service, implementation));
      }

      /// <summary>Resolves the registered implementation for the given type.</summary>
      /// <param name="service">The service. </param>
      /// <returns>The resolved type </returns>
      public object Resolve(Type service)
      {
         if (service.IsGenericType && service.GenericTypeArguments.Length == 1 && typeof(IEnumerable).IsAssignableFrom(service))
            return ResolveListWithElementType(service.GenericTypeArguments[0]);

         var descriptor = entries.FirstOrDefault(x => x.ServiceType == service);
         if (descriptor == null)
            return null;

         return ResolveInternal(descriptor);
      }

      private object ResolveListWithElementType(Type elementType)
      {
         var constructedListType = typeof(List<>).MakeGenericType(elementType);
         var instance = (IList)Activator.CreateInstance(constructedListType);
         if (instance != null)
         {
            foreach (var element in ResolveAll(elementType))
               instance.Add(element);
         }

         return instance;
      }

      private IEnumerable ResolveAll(Type elementType)
      {
         var serviceDescriptors = FindAll(elementType).ToArray();
         foreach (var serviceDescriptor in serviceDescriptors)
            yield return ResolveInternal(serviceDescriptor);
      }

      private IEnumerable<ServiceDescriptor> FindAll(Type service)
      {
         return entries.Where(x => x.ServiceType == service);
      }

      private object ResolveInternal(ServiceDescriptor descriptor)
      {
         if (descriptor.ImplementationInstance != null)
            return descriptor.ImplementationInstance;

         object instance;
         if (descriptor.ImplementationFactory != null)
         {
            instance = descriptor.ImplementationFactory(this);
         }
         else
         {
            instance = BuildInstance(descriptor.ImplementationType, Options.ConstructorSelectionStrategy);
         }

         if (descriptor.Lifetime == ServiceLifetime.Singleton)
         {
            var singleton = ServiceDescriptor.Singleton(descriptor.ServiceType, instance);
            entries.Remove(descriptor);
            entries.Add(singleton);
         }

         return instance;
      }

      /// <summary>Resolves the registered implementation for the given type.</summary>
      /// <typeparam name="T">The service type to resolve </typeparam>
      /// <returns>The resolved type </returns>
      public T Resolve<T>()
         where T : class
      {
         var resolve = Resolve(typeof(T));
         return resolve as T;
      }

      #endregion

      #region Methods

      /// <summary>Activates the instance.</summary>
      /// <param name="type">The type to create. </param>
      /// <param name="args">The args for the constructor. </param>
      /// <returns>The created instance </returns>
      /// <exception cref="ArgumentNullException"><paramref name="type"/> is null. </exception>
      /// <exception cref="ArgumentException">
      ///    <paramref name="type"/> is not a RuntimeType. -or-<paramref name="type"/> is an open generic type (that is, the
      ///    <see cref="P:System.Type.ContainsGenericParameters"/> property returns true).
      /// </exception>
      /// <exception cref="NotSupportedException">
      ///    <paramref name="type"/> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder"/>.-or- Creation of
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and
      ///    <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of those types, is not supported.
      /// </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      /// <exception cref="MethodAccessException">The caller does not have permission to call this constructor. </exception>
      /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
      /// <exception cref="InvalidComObjectException">
      ///    The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or
      ///    "Overload:System.Type.GetTypeFromCLSID".
      /// </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="COMException">
      ///    <paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified
      ///    class is not registered.
      /// </exception>
      /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type. </exception>
      protected virtual object ActivateInstance(Type type, object[] args)
      {
         return args.Length > 0 ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
      }

      /// <summary>Builds the instance.</summary>
      /// <param name="type">The type. </param>
      /// <param name="strategy">The <see cref="ConstructorSelectionStrategy"/> that is used. </param>
      /// <returns>The instance </returns>
      /// <exception cref="ArgumentNullException"><paramref name="type"/> is null. </exception>
      /// <exception cref="ArgumentException">
      ///    <paramref name="type"/> is not a RuntimeType. -or-<paramref name="type"/> is an open generic type (that is, the
      ///    <see cref="P:System.Type.ContainsGenericParameters"/> property returns true).
      /// </exception>
      /// <exception cref="NotSupportedException">
      ///    <paramref name="type"/> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder"/>.-or- Creation of
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and
      ///    <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of those types, is not supported.
      /// </exception>
      /// <exception cref="MethodAccessException">The caller does not have permission to call this constructor. </exception>
      /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
      /// <exception cref="InvalidComObjectException">
      ///    The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or
      ///    "Overload:System.Type.GetTypeFromCLSID".
      /// </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="COMException">
      ///    <paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified
      ///    class is not registered.
      /// </exception>
      /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type. </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      protected object BuildInstance(Type type, ConstructorSelectionStrategy strategy)
      {
         var args = DetermineConstructorArgs(type, strategy);
         if (args == null)
            return null;
         return ActivateInstance(type, args);
      }

      private object[] DetermineConstructorArgs(Type implementation, ConstructorSelectionStrategy selectionStrategy)
      {
         var args = new List<object>();
         var constructor = selectionStrategy.SelectCostructor(implementation);
         if (constructor == null)
            return null;

         args.AddRange(constructor.GetParameters().Select(info => Resolve(info.ParameterType)));
         return args.ToArray();
      }

      #endregion

      public object GetService(Type serviceType)
      {
         return Resolve(serviceType);
      }
   }

   // ReSharper restore ExceptionNotDocumented
}