// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericBootstrapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers
{
   using System;
   using System.Collections.Generic;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;
   using ServiceCollection = Microsoft.Extensions.DependencyInjection.ServiceCollection;

   /// <summary>Bootstrapper for generic <see cref="IApplication{T}"/>s/// </summary>
   /// <typeparam name="T">The type pf the application</typeparam>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.BootStrappers.BootstrapperBase"/>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IBootstrapper{T}"/>
   internal class GenericBootstrapper<T> : BootstrapperBase, IBootstrapper<T>
      where T : class, IApplication
   {
      #region Constants and Fields

      private readonly IServiceCollection serviceCollection = new ServiceCollection();
      
      private Func<IServiceCollection, IServiceProvider> createServiceProvider;

      #endregion

      #region IBootstrapper<T> Members

      /// <summary>
      ///    Specifies the window height of the console window that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.ConsoleWindowHeightAttribute"/>
      /// </summary>
      /// <param name="height">The expected window height.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IBootstrapper`1"/> for further configuration</returns>
      public IBootstrapper<T> SetWindowHeight(int height)
      {
         WindowHeight = height;
         return this;
      }

      /// <summary>
      ///    Specifies the window width of the console window that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.ConsoleWindowWidthAttribute"/>
      /// </summary>
      /// <param name="width">The expected window width.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IBootstrapper`1"/> for further configuration</returns>
      public IBootstrapper<T> SetWindowWidth(int width)
      {
         WindowWidth = width;
         return this;
      }

      public IBootstrapper<T> UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory) 
         where TContainerBuilder : notnull
      {
         if (factory is null)
            throw new ArgumentNullException(nameof(factory));

         createServiceProvider = CreateWithFactory;
         return this;

         IServiceProvider CreateWithFactory(IServiceCollection arg)
         {
            var builder = factory.CreateBuilder(arg);
            return factory.CreateServiceProvider(builder);
         }
      }


      /// <summary>
      ///    Specifies the title of the console window that should be used. NOTE: this overwrites the value specified by the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.ConsoleWindowTitleAttribute"/>
      /// </summary>
      /// <param name="windowTitle">The window title to set.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IBootstrapper`1"/> for further configuration</returns>
      public IBootstrapper<T> SetWindowTitle(string windowTitle)
      {
         WindowTitle = windowTitle;
         return this;
      }

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.IApplication"/> of type <see cref="!:T"/></returns>
      public T Run(string[] args) =>
         CreateApplicationManager()
            .RunAsync(args, CancellationToken.None).GetAwaiter()
            .GetResult();

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments as string.</param>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.IApplication"/> of type <see cref="!:T"/></returns>
      public T Run(string args) =>
         CreateApplicationManager().RunAsync(args, CancellationToken.None)
            .GetAwaiter()
            .GetResult();

      /// <summary>Runs the configured application with the commandline arguments <see cref="Environment.CommandLine"/>.</summary>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.IApplication"/> of type <see cref="!:T"/></returns>
      public T Run() => Run(Environment.CommandLine);

      /// <summary>Runs the configured application with the commandline arguments from <see cref="P:System.Environment.CommandLine"/>.</summary>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication"/> of type <see cref="!:T"/></returns>
      public Task<T> RunAsync(CancellationToken cancellationToken) => RunAsync(Environment.CommandLine, cancellationToken);

      public IBootstrapper<T> ConfigureServices(Action<IServiceCollection> serviceSetup)
      {
         serviceSetup(serviceCollection);
         return this;
      }

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication"/> of type <see cref="!:T"/></returns>
      public Task<T> RunAsync(string[] args, CancellationToken cancellationToken) => CreateApplicationManager().RunAsync(args, cancellationToken);

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments as string. Use <see cref="P:System.Environment.CommandLine"/></param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication"/> of type <see cref="!:T"/></returns>
      public Task<T> RunAsync(string args, CancellationToken cancellationToken) => CreateApplicationManager().RunAsync(args, cancellationToken);

      #endregion

      #region Methods



      private ConsoleApplicationManagerGeneric<T> CreateApplicationManager()
      {
         EnsureRequiredServices();

         var serviceProvider = CreateServiceProvider(serviceCollection);

         var applicationManager = new ConsoleApplicationManagerGeneric<T>(serviceProvider.GetRequiredService<T>)
            {
               WindowTitle = WindowTitle,
               WindowHeight = WindowHeight,
               WindowWidth = WindowWidth
            };

         return applicationManager;
      }

      private void EnsureRequiredServices()
      {
         serviceCollection.AddRequiredServices();
         serviceCollection.AddApplicationTypes<T>();
      }

      private IServiceProvider CreateServiceProvider(IServiceCollection services)
      {
         if (createServiceProvider != null)
            return createServiceProvider(services);

         var factory = new BuildInServiceProviderFactory();
         var collection = factory.CreateBuilder(services);
         return factory.CreateServiceProvider(collection);
      }

      #endregion
   }
}