namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using ConsoLovers.ConsoleToolkit.Core.DIContainer;
    using JetBrains.Annotations;
    using System;

    public class DefaultFactory : IObjectFactory
    {
        #region Private Fields

        private readonly IContainer container;

        #endregion Private Fields

        #region Public Constructors

        public DefaultFactory()
         : this(new Container())
        {
        }

        public DefaultFactory([NotNull] IContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));

            container.Register<IObjectFactory>(this).WithLifetime(Lifetime.Singleton);
            container.Register<ICommandLineEngine, CommandLineEngine>().WithLifetime(Lifetime.Singleton);
            container.Register<IConsole>(new ConsoleProxy()).WithLifetime(Lifetime.Singleton);
        }

        #endregion Public Constructors

        #region Public Methods

        public T CreateInstance<T>()
         where T : class
        {
            return container.Create<T>();
        }

        public object CreateInstance(Type type)
        {
            return container.Create(type);
        }

        public T Resolve<T>()
           where T : class
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        #endregion Public Methods
    }
}