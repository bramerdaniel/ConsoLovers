// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericBootstrapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers
{
   using System;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   /// <summary>Bootstrapper for generic <see cref="IApplication{T}"/>s/// </summary>
   /// <typeparam name="T">The type pf the application</typeparam>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.BootStrappers.BootstrapperBase"/>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IBootstrapper{T}"/>
   internal class GenericBootstrapper<T> : BootstrapperBase, IBootstrapper<T>
      where T : class, IApplication
   {
      #region Constants and Fields

      private Func<T> createApplication;

      #endregion

      #region IBootstrapper<T> Members

      /// <summary>Specifies the function that creates the instance of the application.</summary>
      /// <param name="applicationBuilder">The application builder function.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IBootstrapper`1"/> for further configuration</returns>
      /// <exception cref="InvalidOperationException">ApplicationBuilder function was already specified.</exception>
      /// <exception cref="ArgumentNullException">applicationBuilder</exception>
      public IBootstrapper<T> CreateApplication(Func<T> applicationBuilder)
      {
         if (createApplication != null)
            throw new InvalidOperationException("ApplicationBuilder function was already specified.");

         createApplication = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
         return this;
      }

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
      public T Run(string[] args) => CreateApplicationManager()
         .RunAsync(args).GetAwaiter()
         .GetResult();


      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments as string.</param>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.IApplication"/> of type <see cref="!:T"/></returns>
      public T Run(string args) => CreateApplicationManager().RunAsync(args)
         .GetAwaiter()
         .GetResult();

      /// <summary>Runs the configured application with the commandline arguments <see cref="Environment.CommandLine"/>.</summary>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.IApplication"/> of type <see cref="!:T"/></returns>
      public T Run() => Run(Environment.CommandLine);

      /// <summary>
      /// Runs the configured application with the given commandline arguments.
      /// </summary>
      /// <param name="args">The command line arguments.</param>
      /// <returns>
      /// The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication" /> of type <see cref="!:T" />
      /// </returns>
      public Task<T> RunAsync(string[] args) => CreateApplicationManager().RunAsync(args);

      /// <summary>Runs the configured application with the given commandline arguments.</summary>
      /// <param name="args">The command line arguments as string. Use <see cref="P:System.Environment.CommandLine"/></param>
      /// <returns>The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication"/> of type <see cref="!:T"/></returns>
      public Task<T> RunAsync(string args) => CreateApplicationManager().RunAsync(args);

      /// <summary>
      /// Runs the configured application with the commandline arguments from <see cref="P:System.Environment.CommandLine" />.
      /// </summary>
      /// <returns>
      /// The created <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication" /> of type <see cref="!:T" />
      /// </returns>
      public Task<T> RunAsync() => RunAsync(Environment.CommandLine);

      /// <summary>
      ///    Specifies the <see cref="T:ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.IObjectFactory"/> that is used to create the
      ///    <see cref="T:ConsoLovers.ConsoleToolkit.Core.IApplication"/>.
      /// </summary>
      /// <param name="container">The container.</param>
      /// <returns>The current <see cref="T:ConsoLovers.ConsoleToolkit.Core.IBootstrapper`1"/> for further configuration</returns>
      /// <exception cref="ArgumentNullException">container</exception>
      /// <exception cref="InvalidOperationException">ApplicationBuilder function was already specified.</exception>
      public IBootstrapper<T> UsingFactory(IObjectFactory container)
      {
         if (container == null)
            throw new ArgumentNullException(nameof(container));
         if (createApplication != null)
            throw new InvalidOperationException("ApplicationBuilder function was already specified.");

         createApplication = container.CreateInstance<T>;
         return this;
      }

      #endregion

      #region Methods

      private ConsoleApplicationManagerGeneric<T> CreateApplicationManager()
      {
         if (createApplication == null)
            createApplication = () => new DefaultFactory().CreateInstance<T>();

         var applicationManager =
            new ConsoleApplicationManagerGeneric<T>(createApplication)
            {
               WindowTitle = WindowTitle, WindowHeight = WindowHeight, WindowWidth = WindowWidth
            };
         return applicationManager;
      }

      #endregion
   }
}