// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerEntry.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
    using System;
    using System.Diagnostics;

    /// <summary>Helper class that is representing an container entry</summary>
    [DebuggerDisplay("{Name} => {ServiceType}")]
    public class ContainerEntry : IContainerEntry
    {
        #region Private Fields

        private object instance;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="ContainerEntry"/> class.</summary>
        /// <param name="container">The container.</param>
        public ContainerEntry(Container container)
        {
            Container = container;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets the container.</summary>
        public Container Container { get; }

        /// <summary>Gets or sets the factory methods of the entry.</summary>
        public Func<IContainer, object> FactoryMethod { get; set; }

        /// <summary>Gets the instance.</summary>
        public object Instance
        {
            get
            {
                if (instance == null)
                    instance = FactoryMethod(Container);
                return instance;
            }
        }

        /// <summary>Gets the lifetime.</summary>
        public Lifetime Lifetime { get; private set; }

        /// <summary>Gets or sets the name of the <see cref="ContainerEntry"/>.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the type of the service the <see cref="ContainerEntry"/> stores.</summary>
        public Type ServiceType { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>Clones this instance./// </summary>
        /// <returns>The cloned entry</returns>
        public ContainerEntry Clone()
        {
            return new ContainerEntry(Container) { ServiceType = ServiceType, Name = Name, Lifetime = Lifetime, };
        }

        /// <summary>Sets the lifetime of the created instance.</summary>
        /// <param name="lifetime">The lifetime.</param>
        /// <exception cref="RegistrationException">A singleton can only be registered once</exception>
        public void WithLifetime(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        #endregion Public Methods
    }
}