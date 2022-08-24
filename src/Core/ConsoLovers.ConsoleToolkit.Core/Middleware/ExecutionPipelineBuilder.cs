// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipelineBuilder.cs" company="ConsoLovers">
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

public class ExecutionPipelineBuilder<T>
   where T : class
{
   #region Constants and Fields

   readonly Func<IExecutionContext<T>, CancellationToken, Task> finalStep;

   private readonly IList<IMiddleware<T>> middlewareList;

   #endregion

   #region Constructors and Destructors

   public ExecutionPipelineBuilder([NotNull] Func<IExecutionContext<T>, CancellationToken, Task> finalStep)
   {
      this.finalStep = finalStep ?? throw new ArgumentNullException(nameof(finalStep));
      middlewareList = new List<IMiddleware<T>>();
   }

   public ExecutionPipelineBuilder()
      : this(FinalStep)
   {
   }

   #endregion

   #region Public Methods and Operators

   public ExecutionPipelineBuilder<T> AddMiddleware(IMiddleware<T> middleware)
   {
      middlewareList.Add(middleware);
      return this;
   }

   public Func<IExecutionContext<T>, CancellationToken, Task> Build()
   {
      return CreatePipe();
   }

   #endregion

   #region Methods

   /// <summary>The finals the step of the middleware pipeline.</summary>
   /// <param name="context">The context that was passed.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns></returns>
   private static Task FinalStep(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      return cancellationToken.IsCancellationRequested ? Task.FromCanceled(cancellationToken) : Task.CompletedTask;
   }

   private Func<IExecutionContext<T>, CancellationToken, Task> CreatePipe()
   {
      var reversed = middlewareList.Reverse().ToArray();
      var current = reversed[0];
      current.Next = finalStep;

      foreach (var middleware in reversed.Skip(1))
      {
         middleware.Next = current.ExecuteAsync;
         current = middleware;
      }

      return current.ExecuteAsync;
   }

   #endregion
}