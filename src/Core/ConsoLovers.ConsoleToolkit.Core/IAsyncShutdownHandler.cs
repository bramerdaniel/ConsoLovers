// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncShutdownHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Threading.Tasks;

/// <summary>Async version of the <see cref="IShutdownHandler"/></summary>
public interface IAsyncShutdownHandler
{
   #region Public Methods and Operators

   /// <summary>Notifies the implementer directly before the application will shut down.</summary>
   /// <param name="result">The execution result.</param>
   Task NotifyShutdownAsync(IExecutionResult result);

   #endregion
}