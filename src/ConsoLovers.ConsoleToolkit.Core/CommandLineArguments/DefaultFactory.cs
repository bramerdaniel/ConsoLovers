namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   public class DefaultFactory : IObjectFactory
   {
      private readonly IContainer container;

      public DefaultFactory()
         :this(new Container())
      {
      }

      public DefaultFactory([NotNull] IContainer container)
      {
         this.container = container ?? throw new ArgumentNullException(nameof(container));

         container.Register<IObjectFactory>(this).WithLifetime(Lifetime.Singleton);
         container.Register<ICommandLineEngine, CommandLineEngine>().WithLifetime(Lifetime.Singleton);
         container.Register<ICommandExecutor, CommandExecutor>().WithLifetime(Lifetime.Singleton);
         container.Register<IConsole>(new ConsoleProxy()).WithLifetime(Lifetime.Singleton);
      }

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
   }
}