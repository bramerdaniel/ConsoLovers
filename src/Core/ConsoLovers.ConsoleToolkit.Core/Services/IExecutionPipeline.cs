// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System.Threading;
using System.Threading.Tasks;

internal interface IExecutionPipeline
{
   Task Execute<T>(IExecutionContext<T> context, CancellationToken cancellationToken)
      where T : class;
}