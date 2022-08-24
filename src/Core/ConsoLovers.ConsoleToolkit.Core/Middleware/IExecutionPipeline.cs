// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionPipeline.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System.Threading;
using System.Threading.Tasks;

internal interface IExecutionPipeline<T>
   where T : class
{
   #region Public Methods and Operators

   Task ExecuteAsync(IExecutionContext<T> context, CancellationToken cancellationToken);

   #endregion
}