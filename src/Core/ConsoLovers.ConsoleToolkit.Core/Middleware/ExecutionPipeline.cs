// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipeline.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

internal class ExecutionPipeline<T> : IExecutionPipeline<T>
   where T : class
{
   private Func<IExecutionContext<T>, CancellationToken, Task> finalStep;

   #region Constructors and Destructors

   public ExecutionPipeline([NotNull] IEnumerable<IMiddleware<T>> middleware)
   {
      if (middleware == null)
         throw new ArgumentNullException(nameof(middleware));

      RegisteredMiddleware = middleware.OrderBy(m => m.ExecutionOrder).ToArray();
   }

   #endregion

   #region IExecutionPipeline<T> Members

   public Task ExecuteAsync(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      var builder = new ExecutionPipelineBuilder<T>(FinalStep);
      foreach (var middleware in RegisteredMiddleware)
         builder.AddMiddleware(middleware);

      var initialMiddleware = builder.Build();
      return initialMiddleware(context, cancellationToken);
   }

   #endregion

   #region Properties

   /// <summary>Gets or sets the final step, that is executed after the last middleware.</summary>
   internal Func<IExecutionContext<T>, CancellationToken, Task> FinalStep
   {
      get => finalStep ?? ((_, _) =>  Task.CompletedTask);
      set => finalStep = value;
   }

   /// <summary>Gets the registered middleware sorted by their execution order.</summary>
   internal IMiddleware<T>[] RegisteredMiddleware { get; }

   #endregion
}