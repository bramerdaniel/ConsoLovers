// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericApplicationBuilder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using Microsoft.Extensions.DependencyInjection;

   /// <summary><see cref="IApplicationBuilder{T}"/> for generic <see cref="IApplication{T}"/>s/// </summary>
   /// <typeparam name="T">The type pf the application</typeparam>
   /// <seealso cref="ApplicationBuilderBase"/>
   /// <seealso cref="IApplicationBuilder"/>
   internal class GenericApplicationBuilder<T> : ApplicationBuilderBase, IApplicationBuilder<T>
      where T : class, IApplication
   {
      #region Constructors and Destructors

      public GenericApplicationBuilder()
         : base(typeof(T))
      {
      }

      #endregion

      #region IApplicationBuilder<T> Members

      /// <summary>
      ///    Specifies the window height of the console window that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.ConsoleWindowHeightAttribute"/>
      /// </summary>
      /// <param name="height">The expected window height.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplicationBuilder`1"/> for further configuration</returns>
      public IApplicationBuilder<T> SetWindowHeight(int height)
      {
         WindowHeight = height;
         return this;
      }

      /// <summary>
      ///    Specifies the window width of the console window that should be used. NOTE: this overwrites the values specified by the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.ConsoleWindowWidthAttribute"/>
      /// </summary>
      /// <param name="width">The expected window width.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplicationBuilder`1"/> for further configuration</returns>
      public IApplicationBuilder<T> SetWindowWidth(int width)
      {
         WindowWidth = width;
         return this;
      }

      public IApplicationBuilder<T> UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
      {
         SetServiceProviderFactory(factory);
         return this;
      }

      /// <summary>
      ///    Specifies the title of the console window that should be used. NOTE: this overwrites the value specified by the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.ConsoleWindowTitleAttribute"/>
      /// </summary>
      /// <param name="windowTitle">The window title to set.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplicationBuilder`1"/> for further configuration</returns>
      public IApplicationBuilder<T> SetWindowTitle(string windowTitle)
      {
         WindowTitle = windowTitle;
         return this;
      }

      /// <summary>Runs the configured application with the commandline arguments from <see cref="P:System.Environment.CommandLine"/>.</summary>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication"/> of type <see cref="!:T"/></returns>
      public Task<T> RunAsync(CancellationToken cancellationToken) => RunAsync(Environment.CommandLine, cancellationToken);

      public IApplicationBuilder<T> ConfigureServices(Action<IServiceCollection> serviceSetup)
      {
         serviceSetup(ServiceCollection);
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

      internal ConsoleApplicationManagerGeneric<T> CreateApplicationManager()
      {
         var applicationManager = new ConsoleApplicationManagerGeneric<T>(CreateServiceProvider())
         {
            WindowTitle = WindowTitle, WindowHeight = WindowHeight, WindowWidth = WindowWidth
         };

         return applicationManager;
      }

      #endregion
   }
}