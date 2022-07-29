// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   /// <summary>Base class for console applications with command line parameters or commands</summary>
   /// <typeparam name="T">The type of the parameter class</typeparam>
   /// <seealso cref="IApplication{T}"/>
   /// <seealso cref="IArgumentInitializer{T}"/>
   /// <seealso cref="IExceptionHandler"/>
   public abstract class ConsoleApplication<T> : IApplication<T>, IArgumentInitializer<T>, IExceptionHandler
      where T : class

   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleApplication{T}"/> class.</summary>
      /// <param name="commandLineEngine">The command line engine.</param>
      /// <exception cref="System.ArgumentNullException">commandLineEngine</exception>
      [InjectionConstructor]
      protected ConsoleApplication([NotNull] ICommandLineEngine commandLineEngine)
      {
         CommandLineEngine = commandLineEngine ?? throw new ArgumentNullException(nameof(commandLineEngine));
         CommandLineEngine.UnhandledCommandLineArgument += OnUnhandledCommandLineArgument;
      }

      #endregion

      #region IApplication Members

      public virtual async Task RunAsync()
      {
         if (await CommandExecutor.ExecuteCommandAsync(Arguments))
            return;

         if (HasArguments)
         {
            await RunWithAsync(Arguments);
         }
         else
         {
            await RunWithoutArgumentsAsync();
         }
      }


      /// <summary>Runs the application logic by executing the specified command,
      ///  or calling one of the RunWith or RunWithoutArguments methods.</summary>
      public virtual void Run()
      {
         RunAsync().GetAwaiter().GetResult();
      }

      #endregion

      #region IApplication<T> Members

      /// <summary>
      ///    This method is called when the application was started with command line arguments. NOTE: If there are <see cref="ICommand"/>s specified in the arguments and the
      ///    application is called with one of those, this method is not called any more, because the command is executed.
      /// </summary>
      /// <param name="arguments">The initialized arguments for the application.</param>
      [Obsolete("Use RunWithAsync to execute your logic")]
      public virtual void RunWith(T arguments)
      {
         RunWithAsync(arguments).GetAwaiter().GetResult();
      }

      /// <summary>This method is called when the application was started with command line arguments. NOTE: If there are <see cref="T:ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ICommand" />s specified in the arguments and theapplication is called with one of those. This method is not called any more, because the command is executed instead.
      /// </summary>
      /// <param name="arguments">The initialized arguments for the application.</param>
      /// <returns></returns>
      public virtual Task RunWithAsync(T arguments)
      {
         return Task.CompletedTask;
      }

      #endregion

      #region IArgumentInitializer<T> Members

      /// <summary>This method is responsible for creating the required default arguments. This could e.g. be a empty instance or an instance filled with data from the app.config...</summary>
      /// <returns>The created arguments instance</returns>
      public virtual T CreateArguments()
      {
         if (GetType() == typeof(T))
            return this as T;

         return Activator.CreateInstance<T>();
      }

      public virtual void InitializeFromString(T instance, string args)
      {
         HasArguments = ! string.IsNullOrWhiteSpace(args);
         Arguments = CommandLineEngine.Map(args, instance);

         OnArgumentsInitialized();
      }

      public virtual void InitializeFromArray(T instance, string[] args)
      {
         HasArguments = args != null && args.Length > 0;
         Arguments = CommandLineEngine.Map(args, instance);

         OnArgumentsInitialized();
      }

      #endregion

      #region IExeptionHandler Members

      public virtual bool HandleException(Exception exception)
      {
         if (exception is MissingCommandLineArgumentException missingArgumentException)
         {
            Console.WriteLine("Invalid command line arguments", ConsoleColor.Yellow);
            Console.WriteLine(missingArgumentException.Message, ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("[ARGUMENT HELP]");
            CommandLineEngine.PrintHelp<T>(null);
            Console.WriteLine();
            return true;
         }

         return false;
      }

      #endregion

      #region Public Properties

      public T Arguments { get; private set; }

      public IConsole Console { get; protected set; } = new ConsoleProxy();

      /// <summary>Gets a value indicating whether this application was called with arguments.</summary>
      public bool HasArguments { get; private set; }

      #endregion

      #region Properties

      protected ICommandLineEngine CommandLineEngine { get; }

      #endregion

      #region Public Methods and Operators

      #endregion

      #region Methods

      /// <summary>Called after the arguments were initialized. This is the first method the arguments can be accessed</summary>
      protected virtual void OnArgumentsInitialized()
      {
      }

      /// <summary>
      ///    Called when a command line argument could not be handled (e.g when an argument was misspelled, and therefor could not be mapped to a property in the arguments class). The
      ///    default behavior is to do nothing. This means that it is ignored.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="CommandLineArgumentEventArgs"/> instance containing the event data.</param>
      protected virtual void OnUnhandledCommandLineArgument(object sender, CommandLineArgumentEventArgs e)
      {
      }

      protected virtual async Task RunWithoutArgumentsAsync()
      {
         await RunWithAsync(Arguments);
      }

      protected void WaitForEnter(string waitText = "Press ENTER to continue.")
      {
         Console.WriteLine(waitText);
         Console.ReadLine();
      }

      #endregion
   }
}