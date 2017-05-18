// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerEntry.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;

   /// <summary>Helper class that is representing an container entry</summary>
   [DebuggerDisplay("{Name} => {ServiceType}")]
   public class ContainerEntry : IContainerEntry
   {
      #region Constants and Fields

      private object instance;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ContainerEntry"/> class.</summary>
      /// <param name="container">The container.</param>
      public ContainerEntry(Container container)
      {
         Container = container;
         FactoryMethods = new Stack<Func<IContainer, object>>();
      }

      #endregion

      #region ILifetime Members

      /// <summary>Sets the lifetime of the created instance.</summary>
      /// <param name="lifetime">The lifetime.</param>
      /// <exception cref="RegistrationException">A singleton can only be registered once</exception>
      public void WithLifetime(Lifetime lifetime)
      {
         if (lifetime == Lifetime.Singleton && FactoryMethods.Count > 1)
            throw new RegistrationException("A sigleton can only be registered once");

         Lifetime = lifetime;
      }

      #endregion

      #region INamed Members

      /// <summary>Sets the name of the service entry.</summary>
      /// <param name="name">The name to register the service with</param>
      /// <returns>The fluent configuration continue interface</returns>
      public ILifetime Named(string name)
      {
         if (FactoryMethods.Count == 1)
         {
            Name = name;
            return this;
         }

         var factory = FactoryMethods.Pop();
         return Container.Rename(this, name, factory);
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the container.</summary>
      public Container Container { get; private set; }

      /// <summary>Gets or sets the factory methods of the entry.</summary>
      public Stack<Func<IContainer, object>> FactoryMethods { get; set; }

      /// <summary>Gets the instance.</summary>
      public object Instance
      {
         get
         {
            if (instance == null)
               instance = FactoryMethods.Single()(Container);
            return instance;
         }
      }

      /// <summary>Gets the lifetime.</summary>
      public Lifetime Lifetime { get; private set; }

      /// <summary>Gets or sets the name of the <see cref="ContainerEntry"/>.</summary>
      public string Name { get; set; }

      /// <summary>Gets or sets the type of the service the <see cref="ContainerEntry"/> stores.</summary>
      public Type ServiceType { get; set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Clones this instance./// </summary>
      /// <returns>The cloned entry</returns>
      public ContainerEntry Clone()
      {
         return new ContainerEntry(Container) { ServiceType = ServiceType, Name = Name, Lifetime = Lifetime, };
      }

      #endregion
   }
}