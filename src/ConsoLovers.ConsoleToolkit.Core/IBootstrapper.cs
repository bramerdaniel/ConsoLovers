// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBootstrapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   /// <summary>Interface that provides a fluent configuration api for configuring the <see cref="IApplication"/> that should be started</summary>
   public interface IBootstrapper
   {
      #region Public Methods and Operators

      /// <summary>Specifies the function that creates the instance of the application.</summary>
      /// <param name="applicationBuilder">The application builder function.</param>
      /// <returns>The current <see cref="IBootstrapper"/> for futher configuration</returns>
      IBootstrapper CreateApplication(Func<Type, object> applicationBuilder);

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <returns>The created <see cref="IApplication"/></returns>
      IApplication Run(string[] args);

      /// <summary>Specifies the window size of the console window. that should be used.
      /// NOTE: this overwrites the values specified by the <see cref="ConsoleWindowWidthAttribute"/> and <see cref="ConsoleWindowHeightAttribute"/></summary>
      /// <param name="width">The expected window width.</param>
      /// <param name="height">The expected window height.</param>
      /// <returns>The current <see cref="IBootstrapper"/> for futher configuration</returns>
      IBootstrapper SetWindowSize(int width, int height);

      /// <summary>Specifies the title of the console window that should be used. 
      /// NOTE: this overwrites the value specified by the <see cref="ConsoleWindowTitleAttribute"/></summary>
      /// <param name="windowTitle">The window title to set.</param>
      /// <returns>The current <see cref="IBootstrapper"/> for futher configuration</returns>
      IBootstrapper SetWindowTitle(string windowTitle);

      /// <summary>Specifies the <see cref="IObjectFactory"/> that is used to create the <see cref="IApplication"/>.</summary>
      /// <param name="objectFactory">The container.</param>
      /// <returns>The current <see cref="IBootstrapper"/> for futher configuration</returns>
      IBootstrapper UsingFactory([NotNull] IObjectFactory objectFactory);

      #endregion
   }

   /// <summary>Generic variant of the <see cref="IBootstrapper"/> interface</summary>
   /// <typeparam name="T">The typ of the <see cref="IApplication"/> or <see cref="IApplication{T}"/> to create</typeparam>
   public interface IBootstrapper<T>
      where T : class, IApplication
   {
      #region Public Methods and Operators

      /// <summary>Specifies the function that creates the instance of the application.</summary>
      /// <param name="applicationBuilder">The application builder function.</param>
      /// <returns>The current <see cref="IBootstrapper{T}"/> for futher configuration</returns>
      IBootstrapper<T> CreateApplication(Func<T> applicationBuilder);

      /// <summary>Specifies the window height of the console window that should be used.
      /// NOTE: this overwrites the values specified by the <see cref="ConsoleWindowHeightAttribute"/></summary>
      /// <param name="height">The expected window height.</param>
      /// <returns>The current <see cref="IBootstrapper{T}"/> for futher configuration</returns>
      IBootstrapper<T> SetWindowHeight(int height);

      /// <summary>Specifies the window width of the console window that should be used.
      /// NOTE: this overwrites the values specified by the <see cref="ConsoleWindowWidthAttribute"/></summary>
      /// <param name="width">The expected window width.</param>
      /// <returns>The current <see cref="IBootstrapper{T}"/> for futher configuration</returns>
      IBootstrapper<T> SetWindowWidth(int width);

      /// <summary>Specifies the title of the console window that should be used. 
      /// NOTE: this overwrites the value specified by the <see cref="ConsoleWindowTitleAttribute"/></summary>
      /// <param name="windowTitle">The window title to set.</param>
      /// <returns>The current <see cref="IBootstrapper{T}"/> for futher configuration</returns>
      IBootstrapper<T> SetWindowTitle(string windowTitle);

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <returns>The created <see cref="IApplication"/> of type <see cref="T"/></returns>
      T Run(string[] args);

      /// <summary>Specifies the <see cref="IObjectFactory"/> that is used to create the <see cref="IApplication"/>.</summary>
      /// <param name="container">The container.</param>
      /// <returns>The current <see cref="IBootstrapper{T}"/> for futher configuration</returns>
      IBootstrapper<T> UsingFactory([NotNull] IObjectFactory container);

      #endregion
   }
}