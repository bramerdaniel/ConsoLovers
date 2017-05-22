namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   public class Factory : IDependencyInjectionContainer
   {
      private readonly IContainer container;

      public Factory()
         :this(new Container())
      {
      }

      public Factory([NotNull] IContainer container)
      {
         if (container == null)
            throw new ArgumentNullException(nameof(container));
         this.container = container;

         container.Register<IDependencyInjectionContainer>(this).WithLifetime(Lifetime.Singleton);
         container.Register<ICommandLineEngine, CommandLineEngine>().WithLifetime(Lifetime.Singleton);
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