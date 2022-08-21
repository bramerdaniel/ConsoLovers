﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System;
using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
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

   #endregion

   #region IApplicationBuilder<T> Members

   public IApplicationBuilder<T> UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
   {
      SetServiceProviderFactory(factory);
      return this;
   }

   public IApplicationBuilder<T> ConfigureServices(Action<IServiceCollection> serviceSetup)
   {
      serviceSetup(ServiceCollection);
      return this;
   }

   public IConsoleApplication<T> Build()
   {
      var serviceProvider = CreateServiceProvider();
      var executable = serviceProvider.GetRequiredService<IConsoleApplication<T>>();
      return executable;
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

   protected IServiceCollection ServiceCollection { get; } = new ServiceCollection();

   #endregion

   #region Methods

   internal IServiceProvider CreateServiceProvider()
   {
      EnsureRequiredServices();

      if (createServiceProvider != null)
         return createServiceProvider(ServiceCollection);

      var factory = new BuildInServiceProviderFactory();
      var collection = factory.CreateBuilder(ServiceCollection);
      var serviceProvider = factory.CreateServiceProvider(collection);

      foreach (var configurationAction in serviceConfigurationActions)
         configurationAction(serviceProvider);

      return serviceProvider;
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

   private void AddDefaultMiddleware()
   {
      ServiceCollection.EnsureSingleton<IExecutionPipeline<T>, ExecutionPipeline<T>>();
      ServiceCollection.AddTransient<IMiddleware<IExecutionContext<T>>, ExceptionHandlingMiddleware<T>>();
      ServiceCollection.AddTransient<IMiddleware<IExecutionContext<T>>, ParserMiddleware<T>>();
      ServiceCollection.AddTransient<IMiddleware<IExecutionContext<T>>, MapperMiddleware<T>>();
      ServiceCollection.AddTransient<IMiddleware<IExecutionContext<T>>, ExecutionMiddleware<T>>();
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