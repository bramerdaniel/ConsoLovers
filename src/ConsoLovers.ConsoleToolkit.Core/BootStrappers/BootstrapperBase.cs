// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootstrapperBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers
{
   using System;
   using System.Collections.Generic;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   internal class BootstrapperBase : IServiceConfigurationHandler
   {
      /// <summary>Gets the type of the application.</summary>
      protected Type ApplicationType { get; }

      #region Constants and Fields

      protected readonly IServiceCollection serviceCollection = new ServiceCollection();

      protected Func<IServiceCollection, IServiceProvider> createServiceProvider;

      private readonly List<Action<IServiceProvider>> serviceConfigurationActions = new();

      protected BootstrapperBase([NotNull] Type applicationType)
      {
         ApplicationType = applicationType ?? throw new ArgumentNullException(nameof(applicationType));
      }

      #endregion

      #region IServiceConfigurationHandler Members

      void IServiceConfigurationHandler.ConfigureService<TService>([NotNull] Action<TService> configurationAction)
      {
         if (configurationAction == null)
            throw new ArgumentNullException(nameof(configurationAction));

         AddServiceConfigurationAction(ConfigAction);

         void ConfigAction(IServiceProvider provider)
         {
            var targetService = provider.GetService<TService>();
            if (targetService != null)
               configurationAction(targetService);
         }
      }

      void IServiceConfigurationHandler.ConfigureRequiredService<TService>(Action<TService> configurationAction)
      {
         if (configurationAction == null)
            throw new ArgumentNullException(nameof(configurationAction));

         AddServiceConfigurationAction(ConfigAction);

         void ConfigAction(IServiceProvider provider)
         {
            var targetService = provider.GetRequiredService<TService>();
            configurationAction(targetService);
         }
      }

      #endregion

      #region Properties

      /// <summary>Gets or sets the height of the window.</summary>
      protected int? WindowHeight { get; set; }

      /// <summary>Gets or sets the window title.</summary>
      protected string WindowTitle { get; set; }

      /// <summary>Gets or sets the width of the window.</summary>
      protected int? WindowWidth { get; set; }

      #endregion

      #region Methods

      protected virtual void AddServiceConfigurationAction([NotNull] Action<IServiceProvider> configAction)
      {
         if (configAction == null)
            throw new ArgumentNullException(nameof(configAction));

         serviceConfigurationActions.Add(configAction);
      }

      internal IServiceProvider CreateServiceProvider()
      {
         EnsureRequiredServices();

         if (createServiceProvider != null)
            return createServiceProvider(serviceCollection);

         var factory = new BuildInServiceProviderFactory();
         var collection = factory.CreateBuilder(serviceCollection);
         var serviceProvider = factory.CreateServiceProvider(collection);

         foreach (var configurationAction in serviceConfigurationActions)
            configurationAction(serviceProvider);

         return serviceProvider;
      }

      protected void EnsureRequiredServices()
      {
         serviceCollection.AddRequiredServices();
         serviceCollection.AddApplicationTypes(ApplicationType);
      }

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

      #endregion
   }
}