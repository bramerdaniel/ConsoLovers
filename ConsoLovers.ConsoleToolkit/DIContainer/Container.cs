// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Container.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   #region

   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Runtime.InteropServices;

   using ConsoLovers.ConsoleToolkit.DIContainer.Strategies;

   using JetBrains.Annotations;

   #endregion

   /// <summary>Simple implementation of a dependency injection container</summary>
   public class Container : IExtendedContainer
   {
      // ReSharper disable ExceptionNotDocumented

      #region Constants and Fields

      private readonly List<ContainerEntry> entries = new List<ContainerEntry>();

      private ContainerOptions options = new ContainerOptions();

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="Container"/> class.</summary>
      public Container()
      {
         Register(typeof(IContainer), this);
      }

      /// <summary>Initializes a new instance of the <see cref="Container"/> class.</summary>
      /// <param name="serviceProvider">The service provider. </param>
      public Container(IServiceProvider serviceProvider)
         : this()
      {
         ServiceProvider = serviceProvider;
         Register(typeof(IServiceProvider), serviceProvider);
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the options the <see cref="Container"/> uses.</summary>
      public ContainerOptions Options
      {
         get
         {
            return options;
         }
         set
         {
            options = value;
         }
      }

      #endregion

      #region Properties

      /// <summary>Gets or sets the service provider.</summary>
      protected IServiceProvider ServiceProvider { get; set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Builds up the given object and injects the dependencies.</summary>
      /// <param name="instance">The instance. </param>
      /// <exception cref="ArgumentNullException">instance is <see langword="null"/>.</exception>
      /// <exception cref="TargetException">The object does not match the target type.-or-The property is an instance property, but obj /> is null. </exception>
      /// <exception cref="TargetParameterCountException">The number of parameters in index does not match the number of parameters the indexed property takes. </exception>
      /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
      /// <exception cref="TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The
      ///    <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.</exception>
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
      /// <exception cref="TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The
      ///    <see cref="P:System.Exception.InnerException"/> property indicates the reason for the error.</exception>
      public void BuildUp([NotNull] object instance, PropertySelectionStrategy strategy)
      {
         if (instance == null)
            throw new ArgumentNullException("instance", "Container can not build up a null object");

         IEnumerable<PropertyInfo> injectables = strategy.SelectProperties(instance.GetType());
         foreach (var propertyInfo in injectables.ToList())
         {
            var injectionInstance = ResolveNamed(propertyInfo.PropertyType, ComputeName(propertyInfo));
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
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of
      ///    those types, is not supported.
      /// </exception>
      /// <exception cref="ArgumentException">
      ///    <paramref name="type"/> is not a RuntimeType. -or-<paramref name="type"/> is an open generic type (that is, the
      ///    <see cref="P:System.Type.ContainsGenericParameters"/> property returns true).
      /// </exception>
      /// <exception cref="ArgumentNullException"><paramref name="type"/> is null. </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type. </exception>
      /// <exception cref="COMException"><paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="InvalidComObjectException">The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or "Overload:System.Type.GetTypeFromCLSID". </exception>
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
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of
      ///    those types, is not supported.
      /// </exception>
      /// <exception cref="ArgumentException">
      ///    <paramref name="type"/> is not a RuntimeType. -or-<paramref name="type"/> is an open generic type (that is, the
      ///    <see cref="P:System.Type.ContainsGenericParameters"/> property returns true).
      /// </exception>
      /// <exception cref="ArgumentNullException"><paramref name="type"/> is null. </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type. </exception>
      /// <exception cref="COMException"><paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="InvalidComObjectException">The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or "Overload:System.Type.GetTypeFromCLSID". </exception>
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

      /// <summary>Registers the instance at the container.</summary>
      /// <param name="service">The service to register. </param>
      /// <param name="implementation">The implementation of the service. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register(Type service, object implementation)
      {
         return Register(service, c => implementation);
      }

      /// <summary>Registers the instance at the container.</summary>
      /// <param name="service">The service to register.</param>
      /// <param name="implementation">The implementation of the service.</param>
      /// <param name="name">The name.</param>
      /// <returns>The registered container element used for fluent configuration</returns>
      public IContainerEntry Register(Type service, object implementation, string name)
      {
         return RegisterNamed(service, c => implementation, name);
      }

      /// <summary>Registers the service type with an implementation type.</summary>
      /// <typeparam name="T">The type of the service to register. </typeparam>
      /// <param name="implementation">The implementation of the service. </param>
      /// ///
      /// <returns> The registered container element used for fluent configuration </returns>
      public IContainerEntry Register<T>(object implementation)
         where T : class
      {
         return Register(typeof(T), implementation);
      }

      /// <summary>Registers this instance.</summary>
      /// <typeparam name="TSer">The type to register. </typeparam>
      /// <typeparam name="TImpl">The type of the instance. </typeparam>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register<TSer, TImpl>()
         where TSer : class where TImpl : class
      {
         return Register(typeof(TSer), typeof(TImpl));
      }

      public IContainerEntry RegisterNamed<T>(object implementation, string name)
         where T : class
      {
         return RegisterNamed(typeof(T), implementation, name);
      }

      public IContainerEntry RegisterNamed(Type service, object implementation, string name)
      {
         return RegisterNamed(service, c => implementation, name);
      }

      public IContainerEntry RegisterNamed<T>(Func<IContainer, object> handler, string name)
         where T : class
      {
         return RegisterInternal(typeof(T), handler, name);
      }

      public IContainerEntry RegisterNamed<TService, TImplementation>(string name)
         where TService : class where TImplementation : class
      {
         return RegisterNamed(typeof(TService), typeof(TImplementation), name);
      }

      public IContainerEntry RegisterNamed(Type service, Func<IContainer, object> handler, string name)
      {
         return RegisterInternal(service, handler, name);
      }

      /// <summary>Registers the type at the container.</summary>
      /// <param name="service">The service type to register. </param>
      /// <param name="handler">The the function that constructs the object. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register(Type service, Func<IContainer, object> handler)
      {
         return RegisterInternal(service, handler, null);
      }

      private IContainerEntry RegisterInternal(Type service, Func<IContainer, object> handler, string name)
      {
         var entry = GetOrCreateEntry(service, name);
         entry.FactoryMethods.Push(handler);
         return entry;
      }

      /// <summary>Registers the type at the container.</summary>
      /// <typeparam name="T">The service type </typeparam>
      /// <param name="handler">The the function that constructs the object. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register<T>(Func<IContainer, object> handler)
         where T : class
      {
         return Register(typeof(T), handler);
      }

      /// <summary>Registers the specified service.</summary>
      /// <param name="service">The service. </param>
      /// <param name="implementation">The implementation. </param>
      /// <returns>The registered container element used for fluent configuration </returns>
      public IContainerEntry Register(Type service, Type implementation)
      {
         return Register(service, c => BuildInstance(implementation, Options.ConstructorSelectionStrategy));
      }

      public IContainerEntry RegisterNamed(Type service, Type implementation, string name)
      {
         return RegisterNamed(service, c => BuildInstance(implementation, Options.ConstructorSelectionStrategy), name);
      }

      /// <summary>Resolves the registered implementation for the given type.</summary>
      /// <param name="service">The service. </param>
      /// <returns>The resolved type </returns>
      public object Resolve(Type service)
      {
         return ResolveNamed(service, null);
      }

      /// <summary>Resolves the registered implementation for the given type.</summary>
      /// <typeparam name="T">The service type to resolve </typeparam>
      /// <returns>The resolved type </returns>
      public T Resolve<T>()
         where T : class
      {
         var resolve = Resolve(typeof(T));
         return resolve is T ? (T)resolve : default(T);
      }

      /// <summary>Gets all the instances registered for the given type.</summary>
      /// <param name="service">The service. </param>
      /// <returns>An <see cref="IEnumerable{T}"/> of the registered implementations </returns>
      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public IEnumerable<object> ResolveAll(Type service)
      {
         var typeEntries = entries.Where(x => x.ServiceType == service).ToArray();
         if (typeEntries.Any())
            return typeEntries.SelectMany(t => t.FactoryMethods.Select(x => x(this)));

         if (ServiceProvider != null)
         {
            var instance = ServiceProvider.GetService(service);
            if (instance != null)
               return new[] { instance };
         }

         return new object[0];
      }

      /// <summary>Gets all the instances registered for the given type.</summary>
      /// <typeparam name="T">The service to resolve </typeparam>
      /// <returns>An <see cref="IEnumerable{T}"/> of the registered implementations </returns>
      public IEnumerable<T> ResolveAll<T>()
         where T : class
      {
         return ResolveAll(typeof(T)).OfType<T>();
      }

      /// <summary>Resolves the registered implementation with the given type for the given key.</summary>
      /// <typeparam name="T">The service type to resolve </typeparam>
      /// <param name="name">The key the implementation was registered with. </param>
      /// <returns>The resolved type </returns>
      public T ResolveNamed<T>(string name)
         where T : class
      {
         var resolve = ResolveNamed(typeof(T), name);
         return resolve is T ? (T)resolve : default(T);
      }

      /// <summary>Resolves the registered implementation with the given type for the given key.</summary>
      /// <param name="service">The service type to resolve. </param>
      /// <param name="name">The key the implementation was registered with. </param>
      /// <returns>The resolved type </returns>
      public object ResolveNamed(Type service, string name)
      {
         var entry = GetEntry(service, name);
         if (entry != null)
         {
            return ResolveInstance(entry);
         }

         if (ServiceProvider != null)
         {
            var instance = ServiceProvider.GetService(service);
            if (instance != null)
               return instance;
         }

         if (typeof(Delegate).IsAssignableFrom(service))
         {
            var typeToCreate = service.GetGenericArguments()[0];
            var factoryFactoryType = typeof(FactoryFactory<>).MakeGenericType(typeToCreate);
            var factoryFactoryHost = Activator.CreateInstance(factoryFactoryType);
            var factoryFactoryMethod = factoryFactoryType.GetMethod("Create");
            return factoryFactoryMethod.Invoke(factoryFactoryHost, new object[] { this });
         }

         if (typeof(string).IsAssignableFrom(service))
            return null;

         if (typeof(IEnumerable).IsAssignableFrom(service))
         {
            var listType = service.GetGenericArguments()[0];
            var instances = ResolveAll(listType).ToList();
            var array = Array.CreateInstance(listType, instances.Count);
            for (var i = 0; i < array.Length; i++)
            {
               array.SetValue(instances[i], i);
            }

            return array;
         }

         return null;
      }

      /// <summary>Unregisters the specified <see cref="IContainerEntry"/>.</summary>
      /// <param name="entry">The entry.</param>
      public void Unregister(IContainerEntry entry)
      {
         var entryImp = (ContainerEntry)entry;
         if (entries.Contains(entryImp))
         {
            entries.Remove(entryImp);
         }
      }

      /// <summary>Unregisters this instance of type <typeparamref name="T"/>.</summary>
      /// <typeparam name="T">The type that should be unregistered.</typeparam>
      public void Unregister<T>()
      {
         Unregister(typeof(T), null);
      }

      /// <summary>Unregisters the specified <paramref name="type"/>.</summary>
      /// <param name="type">The <see cref="Type"/> that should be unregistered.</param>
      public void Unregister(Type type)
      {
         Unregister(type, null);
      }

      /// <summary>Unregisters the named instance of type <typeparamref name="T"/>.</summary>
      /// <typeparam name="T">The type that should be unregistered.</typeparam>
      /// <param name="name">The name of the component that should be unregistered.</param>
      public void UnregisterNamed<T>(string name)
      {
         Unregister(typeof(T), name);
      }

      #endregion

      #region Methods

      /// <summary>Renames the specified entry by cloning it.</summary>
      /// <param name="entry">The entry.</param>
      /// <param name="name">The name.</param>
      /// <param name="factory">The factory.</param>
      /// <returns>the fluent configuration followers</returns>
      internal ILifetime CloneEntry(ContainerEntry entry, string name, Func<IContainer, object> factory)
      {
         var clone = entry.Clone();
         clone.FactoryMethods.Push(factory);
         clone.Name = name;
         entries.Add(clone);

         return clone;
      }

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
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of
      ///    those types, is not supported.
      /// </exception>
      /// <exception cref="TargetInvocationException">The constructor being called throws an exception. </exception>
      /// <exception cref="MethodAccessException">The caller does not have permission to call this constructor. </exception>
      /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
      /// <exception cref="InvalidComObjectException">The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or "Overload:System.Type.GetTypeFromCLSID". </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="COMException"><paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
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
      ///    <see cref="T:System.TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="T:System.Void"/>, and <see cref="T:System.RuntimeArgumentHandle"/> types, or arrays of
      ///    those types, is not supported.
      /// </exception>
      /// <exception cref="MethodAccessException">The caller does not have permission to call this constructor. </exception>
      /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism. </exception>
      /// <exception cref="InvalidComObjectException">The COM type was not obtained through Overload:System.Type.GetTypeFromProgID" or "Overload:System.Type.GetTypeFromCLSID". </exception>
      /// <exception cref="MissingMethodException">No matching public constructor was found. </exception>
      /// <exception cref="COMException"><paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered. </exception>
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

         args.AddRange(constructor.GetParameters().Select(info => ResolveNamed(info.ParameterType, ComputeName(info))));
         return args.ToArray();
      }

      private string ComputeName(ParameterInfo info)
      {
         var attribute = info.GetCustomAttribute(typeof(DependencyAttribute)) as DependencyAttribute;
         return attribute?.Name;
      }

      private string ComputeName(PropertyInfo info)
      {
         var attribute = info.GetCustomAttribute(typeof(DependencyAttribute)) as DependencyAttribute;
         return attribute?.Name;
      }

      private ContainerEntry GetEntry(Type service, string name)
      {
         if (service == null && name != null)
         {
            return entries.FirstOrDefault(x => x.Name == name);
         }

         return entries.FirstOrDefault(x => x.ServiceType == service && x.Name == name);
      }

      private ContainerEntry GetOrCreateEntry(Type service, string name)
      {
         var entry = GetEntry(service, name);
         if (entry == null)
         {
            entry = new ContainerEntry(this) { ServiceType = service, Name = name };
            entries.Add(entry);
         }
         else
         {
            throw new InvalidOperationException($"An entry of type {service.FullName} was already registered with the name {name}.");
         }

         return entry;
      }

      private object ResolveInstance(ContainerEntry entry)
      {
         switch (entry.Lifetime)
         {
            case Lifetime.Singleton: return entry.Instance;
            case Lifetime.None: return entry.FactoryMethods.Last()(this);
            default: throw new ArgumentOutOfRangeException();
         }
      }

      #endregion

      private class FactoryFactory<T>
      {
         #region Public Methods and Operators

         // ReSharper disable UnusedMember.Local
         // It is used by reflection
         public Func<T> Create(Container container)
            // ReSharper restore UnusedMember.Local
         {
            return () => (T)container.ResolveNamed(typeof(T), null);
         }

         #endregion
      }

      private void Unregister(Type type, string name)
      {
         var entry = GetEntry(type, name);
         if (entry != null)
         {
            entries.Remove(entry);
         }
      }
   }

   // ReSharper restore ExceptionNotDocumented
}