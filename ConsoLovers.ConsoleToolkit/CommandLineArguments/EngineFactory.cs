namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   public class EngineFactory : IEngineFactory
   {
      private readonly IContainer container;

      public EngineFactory()
         :this(new Container())
      {
      }

      public EngineFactory([NotNull] IContainer container)
      {
         if (container == null)
            throw new ArgumentNullException(nameof(container));
         this.container = container;

         container.Register<IEngineFactory>(this).WithLifetime(Lifetime.Singleton);
         container.Register<ICommandLineEngine, CommandLineEngine>().WithLifetime(Lifetime.Singleton);
      }

      public virtual IArgumentMapper<T> CreateMapper<T>()
         where T : class
      {
         var info = new ArgumentClassInfo(typeof(T));
         return info.HasCommands ? (IArgumentMapper<T>)new CommandMapper<T>(this) : new ArgumentMapper<T>(this);
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
   }
}