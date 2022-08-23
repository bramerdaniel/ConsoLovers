// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System;
using System.Collections.Generic;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Middleware;
using ConsoLovers.ConsoleToolkit.Core.Services;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

internal class ApplicationBuilder<T> : IApplicationBuilder<T>, IServiceConfigurationHandler
   where T : class
{
   #region Constants and Fields

   private readonly List<Action<IServiceProvider>> serviceConfigurationActions = new();

   private Func<IServiceCollection, IServiceProvider> createServiceProvider;

   private IServiceCollection serviceCollection;

   private IServiceProvider serviceProvider;

   #endregion

   #region IApplicationBuilder<T> Members

   public IApplicationBuilder<T> UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
   {
      SetServiceProviderFactory(factory);
      return this;
   }

   public IApplicationBuilder<T> AddService(Action<IServiceCollection> serviceSetup)
   {
      serviceSetup(ServiceCollection);
      return this;
   }

   public IConsoleApplication<T> Build()
   {
      return GetOrCreateServiceProvider().GetRequiredService<IConsoleApplication<T>>();
   }

   public IApplicationBuilder<T> UseServiceCollection([NotNull] IServiceCollection collection)
   {
      if (collection == null)
         throw new ArgumentNullException(nameof(collection));

      // TODO write test for this scenario
      serviceCollection = CopyRegisteredService(collection);
      return this;
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

   protected IServiceCollection ServiceCollection
   {
      get => serviceCollection ??= new ServiceCollection();
   }

   #endregion

   #region Public Methods and Operators

   public IApplicationBuilder<T> ReturnInstance()
   {
      return this;
   }

   #endregion

   #region Methods

   internal IServiceProvider GetOrCreateServiceProvider()
   {
      if (serviceProvider != null)
         return serviceProvider;

      EnsureRequiredServices();

      if (createServiceProvider != null)
         return createServiceProvider(ServiceCollection);

      var factory = new BuildInServiceProviderFactory();
      var collection = factory.CreateBuilder(ServiceCollection);
      var provider = factory.CreateServiceProvider(collection);

      foreach (var configurationAction in serviceConfigurationActions)
         configurationAction(provider);

      serviceProvider = provider;
      return provider;
   }

   protected virtual void AddServiceConfigurationAction([NotNull] Action<IServiceProvider> configAction)
   {
      if (configAction == null)
         throw new ArgumentNullException(nameof(configAction));

      serviceConfigurationActions.Add(configAction);
   }

   protected void EnsureRequiredServices()
   {
      ServiceCollection.AddRequiredServices();
      ServiceCollection.AddArgumentTypes<T>();

      ServiceCollection.EnsureSingleton<IApplicationLogic, DefaultApplicationLogic>();
      ServiceCollection.TryAddSingleton<IConsoleApplication<T>, ConsoleApplication<T>>();

      AddDefaultMiddleware();
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

   private void AddDefaultMiddleware()
   {
      ServiceCollection.EnsureSingleton<IExecutionPipeline<T>, ExecutionPipeline<T>>();
      ServiceCollection.AddTransient<IMiddleware<T>, ExceptionHandlingMiddleware<T>>();
      ServiceCollection.AddTransient<IMiddleware<T>, ParserMiddleware<T>>();
      ServiceCollection.AddTransient<IMiddleware<T>, MapperMiddleware<T>>();
      ServiceCollection.AddTransient<IMiddleware<T>, ExecutionMiddleware<T>>();
   }

   private IServiceCollection CopyRegisteredService(IServiceCollection collection)
   {
      if (serviceCollection != null)
      {
         foreach (var descriptor in serviceCollection)
            collection.Add(descriptor);
      }

      return collection;
   }

   #endregion
}