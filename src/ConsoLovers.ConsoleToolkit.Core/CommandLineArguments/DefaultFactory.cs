namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   public class DefaultFactory : IObjectFactory , IServiceProvider
   {
      private readonly IContainer container;

      public DefaultFactory()
         :this(new Container())
      {
      }

      public DefaultFactory([NotNull] IContainer container)
      {
         this.container = container ?? throw new ArgumentNullException(nameof(container));
      }



      public object CreateInstance(Type type)
      {
         return container.Create(type);
      }

      
      public object GetService(Type serviceType)
      {
         return container.Resolve(serviceType) ?? container.Create(serviceType);
      }
   }
}