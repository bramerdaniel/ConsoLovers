// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineEngine.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

/* Unmerged change from project 'ConsoLovers.ConsoleToolkit.Core (netstandard2.0)'
Before:
namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
After:
namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using System;
*/

/* Unmerged change from project 'ConsoLovers.ConsoleToolkit.Core (net461)'
Before:
namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
After:
namespace ConsoLovers.ConsoleToolkit.Core.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using System;
*/

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Reflection;
   using System.Text;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   /// <summary>Interface for parsing/analyzing command line arguments and mapping them to a specified class</summary>
   public interface ICommandLineEngine
   {
      #region Public Events

      /// <summary>Occurs when command line argument was passed to the <see cref="ICommandLineEngine"/> and it was processed and mapped to a specific property.</summary>
      event EventHandler<CommandLineArgumentEventArgs> HandledCommandLineArgument;

      /// <summary>Occurs when command line argument was passed to the <see cref="ICommandLineEngine"/> the could not be processed in any way.</summary>
      event EventHandler<CommandLineArgumentEventArgs> UnhandledCommandLineArgument;

      #endregion

      #region Public Methods and Operators

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for</typeparam>
      /// <param name="consoleWidth">Width of the console.</param>
      /// <returns>A <see cref="StringBuilder"/> containing the formatted help text.</returns>
      StringBuilder FormatHelp<T>( int consoleWidth);

      /// <summary>Gets the help information for the class of the given type.</summary>
      /// <typeparam name="T">The argument class for creating the help for</typeparam>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      IEnumerable<ArgumentHelp> GetHelp<T>();

      /// <summary>Maps the specified arguments to a class of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string[] args)
         where T : class;

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string[] args, T instance)
         where T : class;

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string args, T instance)
         where T : class;

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for </typeparam>
      void PrintHelp<T>();

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <param name="argumentType">Type of the argument class to print the help for</param>
      void PrintHelp(Type argumentType);

      /// <summary>Prints the help for the given <see cref="propertyInfo"/> to the <see cref="Console"/>.</summary>
      /// <param name="propertyInfo">The <see cref="PropertyInfo"/> to print the help for</param>
      void PrintHelp(PropertyInfo propertyInfo);

      #endregion
   }
}