// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public interface IConsoleApplication : IApplication
   {
      #region Public Methods and Operators

      int Exit();

      void Initialize(string[] args);

      #endregion
   }

   /// <summary>Basic interface for an application that can only run without arguments</summary>
   public interface IApplication
   {
      #region Public Methods and Operators

      /// <summary>Runs the application .</summary>
      void Run();

      #endregion
   }

   /// <summary>An <see cref="IApplication"/> that takes arguments of type <see cref="T"/></summary>
   /// <typeparam name="T">The type of the applications arguments</typeparam>
   public interface IApplication<T> : IApplication where T : class
   {
      #region Public Methods and Operators

      /// <summary>
      ///    This methof is called when the application was started with command line arguments. NOTE: If there are <see cref="ICommand"/>s specified in the arguments and the
      ///    application is called with one of those. This method is not called any more, because the command is executed.
      /// </summary>
      /// <param name="arguments">The initialited arguments for the application.</param>
      void RunWith(T arguments);

      #endregion
   }

   public interface IInitializer
   {
      #region Public Methods and Operators

      void Initialize(string[] args);

      #endregion
   }

   public interface IArgumentInitializer<T>
      where T : class
   {
      #region Public Methods and Operators

      /// <summary>This method is responsible for creating the required default arguments.
      /// This could e.g. be a empty instance or an instance filledd with data from the app.config...</summary>
      /// <returns>The created arguments instance</returns>
      T CreateArguments();

      /// <summary>Initializes the given instance with the command line arguments array.</summary>
      /// <param name="instance">The instance to fill.</param>
      /// <param name="args">The arguments to use.</param>
      void InitializeArguments(T instance, string[] args);

      #endregion
   }

   public interface IExeptionHandler
   {
      #region Public Methods and Operators

      /// <summary>Handler method the should check if it is an known error e.g. missing command line argument</summary>
      /// <param name="exception">The exception that occured.</param>
      /// <returns>True if the exception was handled, otherwise false</returns>
      bool HandleException(Exception exception);

      #endregion
   }
}