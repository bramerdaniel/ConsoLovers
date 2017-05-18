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