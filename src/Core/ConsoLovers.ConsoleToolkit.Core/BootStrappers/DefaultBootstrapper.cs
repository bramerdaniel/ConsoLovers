// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultBootstrapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers
{
   using System;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   /// <summary>The default <see cref="IBootstrapper"/> for non generic applications</summary>
   /// <seealso cref="IBootstrapper"/>
   internal class DefaultBootstrapper : BootstrapperBase, IBootstrapper
   {
      
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="DefaultBootstrapper"/> class.</summary>
      /// <param name="applicationType">Type of the application.</param>
      /// <exception cref="System.ArgumentNullException">applicationType</exception>
      public DefaultBootstrapper([NotNull] Type applicationType)
         : base(applicationType)
      {
      }

      #endregion

      #region IBootstrapper Members

      public IApplication Run(string[] args) => CreateApplicationManager().Run(args);

      public IApplication Run() => CreateApplicationManager().Run(Environment.CommandLine);

      public IBootstrapper SetWindowSize(int width, int height)
      {
         WindowWidth = width;
         WindowHeight = height;
         return this;
      }

      public IBootstrapper SetWindowTitle(string windowTitle)
      {
         WindowTitle = windowTitle;
         return this;
      }

      public IBootstrapper ConfigureServices([NotNull] Action<IServiceCollection> serviceSetup)
      {
         if (serviceSetup == null)
            throw new ArgumentNullException(nameof(serviceSetup));

         serviceSetup(ServiceCollection);
         return this;
      }

      public IBootstrapper UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
      {
         SetServiceProviderFactory(factory);
         return this;
      }

      #endregion

      #region Methods

      private ConsoleApplicationManager CreateApplicationManager()
      {
         var applicationManager = new ConsoleApplicationManager(ApplicationType, CreateServiceProvider()) { WindowTitle = WindowTitle };
         return applicationManager;
      }

      #endregion
   }
}