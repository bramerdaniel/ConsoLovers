// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using Microsoft.Extensions.DependencyInjection;

   /// <summary>Interface that provides a fluent configuration api for configuring the <see cref="IApplication"/> that should be started</summary>
   public interface IApplicationBuilder : IDependencyInjectionAbstraction<IApplicationBuilder>
   {
      #region Public Methods and Operators

      /// <summary>Runs the configured application with the commandline arguments from <see cref="Environment.CommandLine"/>.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns>The created <see cref="IApplication"/></returns>
      Task<IApplication> RunAsync(string[] args, CancellationToken cancellationToken);

      /// <summary>Runs the configured application with the commandline arguments from <see cref="Environment.CommandLine"/>.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns>The created <see cref="IApplication"/></returns>
      Task<IApplication> RunAsync(string args, CancellationToken cancellationToken);

      /// <summary>
      ///    Specifies the window size of the console window. that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="ConsoleWindowWidthAttribute"/> and <see cref="ConsoleWindowHeightAttribute"/>
      /// </summary>
      /// <param name="width">The expected window width.</param>
      /// <param name="height">The expected window height.</param>
      /// <returns>The current <see cref="IApplicationBuilder"/> for further configuration</returns>
      IApplicationBuilder SetWindowSize(int width, int height);

      /// <summary>
      ///    Specifies the title of the console window that should be used. NOTE: this overwrites the value specified by the
      ///    <see cref="ConsoleWindowTitleAttribute"/>
      /// </summary>
      /// <param name="windowTitle">The window title to set.</param>
      /// <returns>The current <see cref="IApplicationBuilder"/> for further configuration</returns>
      IApplicationBuilder SetWindowTitle(string windowTitle);

      #endregion
   }

   /// <summary>Generic variant of the <see cref="IApplicationBuilder"/> interface</summary>
   /// <typeparam name="T">The typ of the <see cref="IApplication"/> or <see cref="IApplication{T}"/> to create</typeparam>
   public interface IApplicationBuilder<T> : IDependencyInjectionAbstraction<IApplicationBuilder<T>>
      where T : class, IApplication
   {
      #region Public Methods and Operators
      
      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns>The created <see cref="IApplication"/> of type <see cref="T"/></returns>
      Task<T> RunAsync(string[] args, CancellationToken cancellationToken);

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments as string. Use <see cref="Environment.CommandLine"/></param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns>The created <see cref="IApplication"/> of type <see cref="T"/></returns>
      Task<T> RunAsync(string args, CancellationToken cancellationToken);

      /// <summary>Runs the configured application with the commandline arguments from <see cref="Environment.CommandLine"/>.</summary>
      /// <returns>The created <see cref="IApplication"/> of type <see cref="T"/></returns>
      Task<T> RunAsync(CancellationToken cancellationToken);

      /// <summary>
      ///    Specifies the window height of the console window that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="ConsoleWindowHeightAttribute"/>
      /// </summary>
      /// <param name="height">The expected window height.</param>
      /// <returns>The current <see cref="IApplicationBuilder"/> for further configuration</returns>
      IApplicationBuilder<T> SetWindowHeight(int height);

      /// <summary>
      ///    Specifies the title of the console window that should be used. NOTE: this overwrites the value specified by the
      ///    <see cref="ConsoleWindowTitleAttribute"/>
      /// </summary>
      /// <param name="windowTitle">The window title to set.</param>
      /// <returns>The current <see cref="IApplicationBuilder"/> for further configuration</returns>
      IApplicationBuilder<T> SetWindowTitle(string windowTitle);

      /// <summary>
      ///    Specifies the window width of the console window that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="ConsoleWindowWidthAttribute"/>
      /// </summary>
      /// <param name="width">The expected window width.</param>
      /// <returns>The current <see cref="IApplicationBuilder"/> for further configuration</returns>
      IApplicationBuilder<T> SetWindowWidth(int width);


      IApplicationBuilder<T> UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory);

      #endregion
   }
}