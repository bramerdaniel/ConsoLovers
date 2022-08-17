namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers
{
   using System;

   using Microsoft.Extensions.DependencyInjection;

   internal class BootstrapperBase
   {
      protected readonly IServiceCollection serviceCollection = new ServiceCollection();

      protected Func<IServiceCollection, IServiceProvider> createServiceProvider;

      /// <summary>Gets or sets the height of the window.</summary>
      protected int? WindowHeight { get; set; }

      /// <summary>Gets or sets the window title.</summary>
      protected string WindowTitle { get; set; }

      /// <summary>Gets or sets the width of the window.</summary>
      protected int? WindowWidth { get; set; }

      protected void SetServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
         where TContainerBuilder : notnull
      {
         if (factory is null)
            throw new ArgumentNullException(nameof(factory));

         createServiceProvider = CreateWithFactory;

         IServiceProvider CreateWithFactory(IServiceCollection arg)
         {
            var builder = factory.CreateBuilder(arg);
            return factory.CreateServiceProvider(builder);
         }
      }

      protected void EnsureRequiredServices(Type applicationType)
      {
         serviceCollection.AddRequiredServices();
         serviceCollection.AddApplicationTypes(applicationType);
      }

      protected IServiceProvider CreateServiceProvider()
      {
         if (createServiceProvider != null)
            return createServiceProvider(serviceCollection);

         var factory = new BuildInServiceProviderFactory();
         var collection = factory.CreateBuilder(serviceCollection);
         return factory.CreateServiceProvider(collection);
      }
   }
}