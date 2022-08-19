// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultApplicationBuilder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   /// <summary>The default <see cref="IApplicationBuilder"/> for non generic applications</summary>
   /// <seealso cref="IApplicationBuilder"/>
   internal class DefaultApplicationBuilder : ApplicationBuilderBase, IApplicationBuilder
   {
      
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="DefaultApplicationBuilder"/> class.</summary>
      /// <param name="applicationType">Type of the application.</param>
      /// <exception cref="System.ArgumentNullException">applicationType</exception>
      public DefaultApplicationBuilder([NotNull] Type applicationType)
         : base(applicationType)
      {
      }

      #endregion

      #region IApplicationBuilder Members

      public IApplicationBuilder SetWindowSize(int width, int height)
      {
         WindowWidth = width;
         WindowHeight = height;
         return this;
      }

      public IApplicationBuilder SetWindowTitle(string windowTitle)
      {
         WindowTitle = windowTitle;
         return this;
      }

      public IApplicationBuilder ConfigureServices([NotNull] Action<IServiceCollection> serviceSetup)
      {
         if (serviceSetup == null)
            throw new ArgumentNullException(nameof(serviceSetup));

         serviceSetup(ServiceCollection);
         return this;
      }

      public IApplicationBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
      {
         SetServiceProviderFactory(factory);
         return this;
      }

      public Task<IApplication> RunAsync(string[] args, CancellationToken cancellationToken)
      {
         return CreateApplicationManager().RunAsync(args, cancellationToken);
      }

      public Task<IApplication> RunAsync(string args, CancellationToken cancellationToken)
      {
         return CreateApplicationManager().RunAsync(args, cancellationToken);
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