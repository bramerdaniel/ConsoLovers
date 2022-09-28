// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IShutdownHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Builders;

/// <summary>Implementers get notified when the <see cref="IConsoleApplication{T}"/> is about to exit</summary>
public interface IShutdownHandler
{
   #region Public Methods and Operators

   /// <summary>Notifies the implementer directly before the application will shut down.</summary>
   /// <param name="result">The execution result.</param>
   void NotifyShutdown(IExecutionResult result);

   #endregion
}