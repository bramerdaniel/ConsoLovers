// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExitCodeHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

public interface IExitCodeHandler
{
   #region Public Methods and Operators

   /// <summary>Called when the <see cref="IExceptionHandler"/> caught an exception</summary>
   /// <param name="result">The context result.</param>
   /// <param name="exception">The <see cref="Exception"/> that occurred.</param>
   void HandleError(IExecutionResult result, Exception exception);

   /// <summary>Called when the execution did not throw any exception</summary>
   /// <param name="result">The execution result.</param>
   void HandleSuccess(IExecutionResult result);

   #endregion
}