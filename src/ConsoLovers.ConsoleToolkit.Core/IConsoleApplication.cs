// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   /// <summary>Basic interface for an application that can only run without arguments</summary>
   public interface IApplication
   {
      #region Public Methods and Operators

      /// <summary>Runs the application logic.</summary>
      void Run();

      #endregion
   }

   /// <summary>An <see cref="IApplication"/> that takes arguments of type <see cref="T"/></summary>
   /// <typeparam name="T">The type of the applications arguments</typeparam>
   public interface IApplication<T> : IApplication where T : class
   {
      #region Public Methods and Operators

      /// <summary>
      ///    This method is called when the application was started with command line arguments. 
      ///    NOTE: If there are <see cref="ICommand"/>s specified in the arguments and the
      ///    application is called with one of those. This method is not called any more, because the command is executed.
      /// </summary>
      /// <param name="arguments">The initialited arguments for the application.</param>
      void RunWith(T arguments);

      #endregion
   }
}