// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationBootstrapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers
{
   using System;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   /// <summary>The default <see cref="IBootstrapper"/> for non generic applications</summary>
   /// <seealso cref="IBootstrapper" />
   internal class DefaultBootstrapper : BootstrapperBase, IBootstrapper
   {
      #region Constants and Fields

      private readonly Type applicationType;


      #endregion

      #region Constructors and Destructors

      public DefaultBootstrapper([NotNull] Type applicationType)
      {
         this.applicationType = applicationType ?? throw new ArgumentNullException(nameof(applicationType));
      }

      #endregion

      #region IBootstrapper Members


      public IApplication Run(string[] args)
      {

         var applicationManager = new ConsoleApplicationManager(CreateServiceProvider())
         {
            WindowTitle = WindowTitle,
         };

         return applicationManager.Run(applicationType, args);
      }

      public IApplication Run()
      {
         var applicationManager = new ConsoleApplicationManager(CreateServiceProvider())
         {
            WindowTitle = WindowTitle,
         };

         return applicationManager.Run(applicationType, Environment.CommandLine);
      }

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

      public IBootstrapper UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
      {
         SetServiceProviderFactory(factory);
         return this;
      }

      #endregion
   }
}