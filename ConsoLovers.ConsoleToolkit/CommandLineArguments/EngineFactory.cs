namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   using ConsoLovers.ConsoleToolkit.DIContainer;

   public class EngineFactory : IEngineFactory
   {
      private readonly IContainer container;

      public EngineFactory()
      {
         container = new Container();
         container.Register<IEngineFactory>(this).WithLifetime(Lifetime.Singleton);
      }

      public virtual IArgumentMapper<T> CreateMapper<T>()
         where T : class
      {
         var info = new ArgumentClassInfo(typeof(T));
         return info.HasCommands ? (IArgumentMapper<T>)new CommandMapper<T>(this) : new ArgumentMapper<T>(this);
      }

      public T CreateArgumentInstance<T>()
         where T : class
      {
         return container.Create<T>();
      }

      public object CreateArgumentInstance(Type type)
      {
         return container.Create(type);
      }

      public object CreateApplication(Type type)
      {
         return container.Create(type);
      }
   }
}