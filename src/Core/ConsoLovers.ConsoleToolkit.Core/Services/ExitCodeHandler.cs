// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCodeHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

/// <summary>Default implementation of the <see cref="IExitCodeHandler"/> interface</summary>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IExitCodeHandler"/>
internal sealed class ExitCodeHandler : IExitCodeHandler
{
   #region IExitCodeHandler Members

   /// <summary>Called when the <see cref="IExceptionHandler"/> caught an exception</summary>
   /// <param name="result">The context result.</param>
   /// <param name="exception">The <see cref="Exception"/> that occurred.</param>
   public void HandleError(IExecutionResult result, Exception exception)
   {
      result.ExitCode = exception switch
      {
         CommandLineArgumentException => -1,
         _ => 1
      };

      Environment.ExitCode = result.ExitCode.GetValueOrDefault(0);
   }

   /// <summary>Called when the execution did not throw any exception</summary>
   /// <param name="result">The execution result.</param>
   public void HandleSuccess(IExecutionResult result)
   {
      result.ExitCode = 0;
      Environment.ExitCode = result.ExitCode.GetValueOrDefault(0);
   }

   #endregion
}