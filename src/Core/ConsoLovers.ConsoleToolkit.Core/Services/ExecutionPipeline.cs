// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using JetBrains.Annotations;

internal class ExecutionPipeline<T> : IExecutionPipeline<T>
      where T : class
{

   private readonly IMiddleware<IExecutionContext<T>>[] middlewares;

   public ExecutionPipeline(IEnumerable<IMiddleware<IExecutionContext<T>>> middlewares)
   {
      // TODO sort middleware tests
      this.middlewares = middlewares.OrderBy(m => m.ExecutionOrder).ToArray();
   }

   public Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      var builder = new PipeBuilder<IExecutionContext<T>>();
      foreach (var middleware in middlewares)
         builder.AddMiddleware(middleware);

      var initialMiddleware = builder.Build();
      return initialMiddleware(context, cancellationToken);
   }
}