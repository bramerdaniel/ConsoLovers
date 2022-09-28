// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IShutdownNotifier.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Threading.Tasks;

public interface IShutdownNotifier
{
   /// <summary>Notifies all <see cref="IShutdownHandler"/>s that the shut down is about to happen.</summary>
   /// <param name="result">The <see cref="IExecutionResult"/>.</param>
   /// <returns>The task the handlers will be called on</returns>
   Task NotifyShutdownAsync(IExecutionResult result);
}