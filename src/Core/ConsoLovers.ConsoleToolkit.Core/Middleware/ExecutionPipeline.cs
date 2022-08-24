// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipeline.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
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
   #region Constants and Fields

   private readonly IMiddleware<T>[] middlewares;

   #endregion

   #region Constructors and Destructors

   public ExecutionPipeline(IEnumerable<IMiddleware<T>> middlewares)
   {
      // TODO sort middleware tests
      // TODO make the build in middlwares replaceable
      this.middlewares = middlewares.OrderBy(m => m.ExecutionOrder).ToArray();
   }

   #endregion

   #region IExecutionPipeline<T> Members

   public Task ExecuteAsync(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      var builder = new ExecutionPipelineBuilder<T>();
      foreach (var middleware in middlewares)
         builder.AddMiddleware(middleware);

      var initialMiddleware = builder.Build();
      return initialMiddleware(context, cancellationToken);
   }

   #endregion
}