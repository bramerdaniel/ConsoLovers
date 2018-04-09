﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationWith.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.Contracts;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   /// <summary>Base class for console applications with command line parameters</summary>
   /// <typeparam name="T">The type of the parameter class</typeparam>
   /// <seealso cref="IApplication{T}"/>
   /// <seealso cref="IArgumentInitializer{T}"/>
   /// <seealso cref="IExeptionHandler"/>
   public abstract class ConsoleApplicationWith<T> : IApplication<T>, IArgumentInitializer<T>, IExeptionHandler
      where T : class

   {
      #region Constructors and Destructors

      [InjectionConstructor]
      protected ConsoleApplicationWith([NotNull] ICommandLineEngine commandLineEngine)
      {
         CommandLineEngine = commandLineEngine ?? throw new ArgumentNullException(nameof(commandLineEngine));
         CommandLineEngine.UnhandledCommandLineArgument += OnUnhandledCommandLineArgument;
      }

      #endregion

      #region IApplication Members

      public virtual void Run()
      {
         var commandExecuted = ExecuteCommand(true);
         if (!commandExecuted)
         {
            if (HasArguments)
            {
                RunWith(Arguments);
            }
            else
            {
               RunWithoutArguments();
            }
         }
      }

      #endregion

      #region IApplication<T> Members

      /// <summary>
      ///    This method is called when the application was started with command line arguments. NOTE: If there are <see cref="ICommand"/>s specified in the arguments and the
      ///    application is called with one of those, this method is not called any more, because the command is executed.
      /// </summary>
      /// <param name="arguments">The initialited arguments for the application.</param>
      public abstract void RunWith(T arguments);

      #endregion

      #region IArgumentInitializer<T> Members

      /// <summary>
      /// This method is responsible for creating the required default arguments.
      /// This could e.g. be a empty instance or an instance filledd with data from the app.config...
      /// </summary>
      /// <returns>
      /// The created arguments instance
      /// </returns>
      public virtual T CreateArguments()
      {
         if (GetType() == typeof(T))
            return this as T;

         return Activator.CreateInstance<T>();
      }

      public virtual void InitializeArguments(T instance, string[] args)
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
            WaitForEnter();
            return true;
         }

         return false;
      }

      #endregion

      #region Public Properties

      public IConsole Console { get; protected set; } = new ConsoleProxy();

      public T Arguments { get; private set; }

      /// <summary>Gets a value indicating whether this application was called with arguments.</summary>
      public bool HasArguments { get; private set; }

      #endregion

      #region Properties

      protected ICommandLineEngine CommandLineEngine { get; }


      #endregion

      #region Public Methods and Operators

      public virtual void RunWithCommand(ICommand command)
      {
         command.Execute();
      }

      #endregion

      #region Methods

      /// <summary>
      ///    Executes the command that was specified in the command line arguments. If no argument was specified but the IsDefaultCommand property was set at one of the commands, a
      ///    simulated command
      /// </summary>
      /// <param name="useDefaultCommand">if set to <c>true</c> [use default command].</param>
      /// <returns></returns>
      protected virtual bool ExecuteCommand(bool useDefaultCommand)
      {
         var applicationArguments = ArgumentClassInfo.FromType<T>();
         if (!applicationArguments.HasCommands)
            return false;

         ICommand command = GetMappedCommand();
         if (command == null)
            return false;

         RunWithCommand(command);
         return true;
      }

      protected ICommand GetMappedCommand()
      {
         if (Arguments == null)
            return null;

         foreach (var propertyInfo in typeof(T).GetProperties())
         {
            if (propertyInfo.PropertyType.GetInterface(typeof(ICommand).FullName) != null)
            {
               if (propertyInfo.GetValue(Arguments) is ICommand value)
                  return value;
            }
         }

         return null;
      }

      /// <summary>Called when after the arguments were initialized. This is the first method the arguments can be accessed</summary>
      protected virtual void OnArgumentsInitialized()
      {
      }

      /// <summary>
      ///    Called when a command line argument could not be handled (e.g when an argument was misspelled, and therfore could not be mapped to a property in the arguments class). The
      ///    default behavior is to do nothing. This means that it is ignored.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="UnhandledCommandLineArgumentEventArgs"/> instance containing the event data.</param>
      protected virtual void OnUnhandledCommandLineArgument(object sender, UnhandledCommandLineArgumentEventArgs e)
      {
      }

      protected virtual void RunWithoutArguments()
      {
         RunWith(Arguments);
      }

      protected void WaitForEnter(string waitText = "Press ENTER to continue.")
      {
         Console.WriteLine(waitText);
         Console.ReadLine();
      }

      #endregion
   }
}