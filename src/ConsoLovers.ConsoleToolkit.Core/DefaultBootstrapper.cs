// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationBootstrapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   /// <summary>The default <see cref="IBootstrapper"/> for non generic applications</summary>
   /// <seealso cref="IBootstrapper" />
   internal class DefaultBootstrapper : BootstrapperBase, IBootstrapper
   {
      #region Constants and Fields

      private readonly Type applicationType;

      private Func<Type, object> createApplication;

      #endregion

      #region Constructors and Destructors

      public DefaultBootstrapper([NotNull] Type applicationType)
      {
         if (applicationType == null)
            throw new ArgumentNullException(nameof(applicationType));

         this.applicationType = applicationType;
      }

      #endregion

      #region IBootstrapper Members

      public IBootstrapper CreateApplication(Func<Type, object> applicationBuilder)
      {
         if (createApplication != null)
            throw new InvalidOperationException("ApplicationBuilder function was already specified.");

         createApplication = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
         return this;
      }

      public IApplication Run(string[] args)
      {
         if (createApplication == null)
            createApplication = new DefaultFactory().CreateInstance;

         var applicationManager = new ConsoleApplicationManager(createApplication)
         {
            WindowTitle = WindowTitle,
            
         };

         return applicationManager.Run(applicationType, args);
      }

      public IBootstrapper UsingFactory(IObjectFactory objectFactory)
      {
         if (objectFactory == null)
            throw new ArgumentNullException(nameof(objectFactory));
         if (createApplication != null)
            throw new InvalidOperationException("ApplicationBuilder function was already specified.");

         createApplication = objectFactory.CreateInstance;
         return this;
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

      #endregion
   }
}