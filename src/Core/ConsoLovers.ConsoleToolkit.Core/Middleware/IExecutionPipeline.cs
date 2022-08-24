// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System.Threading;
using System.Threading.Tasks;

internal interface IExecutionPipeline<T>
      where T : class
{
   Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken);
}