// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

internal class ExecutionPipeline<T> : IExecutionPipeline<T>
      where T : class
{
   private readonly IMiddleware<T>[] middlewares;

   public ExecutionPipeline(IEnumerable<IMiddleware<T>> middlewares)
   {
      // TODO sort middleware tests
      this.middlewares = middlewares.OrderBy(m => m.ExecutionOrder).ToArray();
   }

   public Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      var builder = new ExecutionPipelineBuilder<T>();
      foreach (var middleware in middlewares)
         builder.AddMiddleware(middleware);

      var initialMiddleware = builder.Build();
      return initialMiddleware(context, cancellationToken);
   }
}