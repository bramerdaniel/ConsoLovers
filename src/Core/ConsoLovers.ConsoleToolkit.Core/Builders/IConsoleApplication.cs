// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System.Threading;
using System.Threading.Tasks;

/// <summary>Interface that represents the application that will be executed</summary>
/// <typeparam name="T"></typeparam>
public interface IConsoleApplication<T>
   where T : class
{
   #region Public Properties

   /// <summary>Gets the arguments of the application.</summary>
   T Arguments { get; }

   /// <summary>Gets the result of the execution.</summary>
   IExecutionResult Result { get; }

   #endregion

   #region Public Methods and Operators

   /// <summary>Runs the application with a string as argument.</summary>
   /// <param name="args">The command line arguments.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns>The application</returns>
   Task<IConsoleApplication<T>> RunAsync(string args, CancellationToken cancellationToken);

   /// <summary>Runs the application with a string array as argument.</summary>
   /// <param name="args">The command line arguments.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns>The application</returns>
   Task<IConsoleApplication<T>> RunAsync(string[] args, CancellationToken cancellationToken);

   #endregion
}