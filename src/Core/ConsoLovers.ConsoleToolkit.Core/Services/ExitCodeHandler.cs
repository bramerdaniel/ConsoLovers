// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCodeHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

/// <summary>Base class for <see cref="IExitCodeHandler"/> implementations</summary>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IExitCodeHandler"/>
public abstract class ExitCodeHandler : IExitCodeHandler
{
   #region IExitCodeHandler Members

   /// <summary>Called when the <see cref="IExceptionHandler"/> caught an exception</summary>
   /// <param name="result">The context result.</param>
   /// <param name="exception">The <see cref="Exception"/> that occurred.</param>
   public virtual void HandleError(IExecutionResult result, Exception exception)
   {
      result.ExitCode = ComputeExitCode(exception);
      SetEnvironmentExitCode(result, result.ExitCode);
   }

   /// <summary>Called when the execution did not throw any exception</summary>
   /// <param name="result">The execution result.</param>
   public virtual void HandleSuccess(IExecutionResult result)
   {
      result.ExitCode = 0;
      SetEnvironmentExitCode(result, result.ExitCode);
   }

   #endregion

   #region Methods

   protected virtual int ComputeExitCode(Exception exception)
   {
      return exception switch
      {
         CommandLineArgumentException commandLineArgumentException => HandleCommandLineArgumentExceptions(commandLineArgumentException),
         _ => HandleOtherExceptions(exception)
      };
   }

   /// <summary>Handles all exit codes for <see cref="CommandLineArgumentException"/>s and derived exception classes.</summary>
   /// <param name="exception">The command line argument exception.</param>
   /// <returns></returns>
   protected virtual int HandleCommandLineArgumentExceptions(CommandLineArgumentException exception)
   {
      return exception switch
      {
         MissingCommandLineArgumentException => -1,
         CommandLineArgumentValidationException => -2,
         CommandLineAttributeException => -3,
         AmbiguousCommandLineArgumentsException => -4,
         InvalidValidatorUsageException => -5,

         _ => -6
      };
   }

   /// <summary>Handles all other exceptions than <see cref="CommandLineArgumentException"/>s.</summary>
   /// <param name="exception">The exception.</param>
   /// <returns></returns>
   protected virtual int HandleOtherExceptions(Exception exception)
   {
      return 1;
   }

   /// <summary>Sets the exit code to the <see cref="Environment"/>.ExitCode property if it has a value.</summary>
   /// <param name="result">The result.</param>
   /// <param name="exitCode">The exit code.</param>
   protected virtual void SetEnvironmentExitCode(IExecutionResult result, int? exitCode)
   {
      if (exitCode.HasValue)
         Environment.ExitCode = exitCode.Value;
   }

   #endregion
}