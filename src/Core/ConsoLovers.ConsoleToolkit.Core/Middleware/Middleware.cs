// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Middleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Base class for a <see cref="IMiddleware{TContext}"/></summary>
/// <typeparam name="TArgs">The type of the context.</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.Middleware.IMiddleware&lt;TContext&gt;"/>
public abstract class Middleware<TArgs> : IMiddleware<TArgs>
   where TArgs : class
{
   #region IMiddleware<TContext> Members

   protected virtual MiddlewareLocation Placement => MiddlewareLocation.BeforeExecution;

   public virtual int ExecutionOrder => (int)Placement;

   /// <summary>Gets the next <see cref="T:ConsoLovers.ConsoleToolkit.Core.Middleware.Middleware`1"/> delegate to invoke.</summary>
   public Func<IExecutionContext<TArgs>, CancellationToken, Task> Next { get; set; }

   /// <summary>The execution handler of the middleware.</summary>
   /// <param name="context">The context that is passed along the execution pipeline.</param>
   /// <param name="cancellationToken">The cancellation token for canceling the execution.</param>
   /// <returns>The execution <see cref="T:System.Threading.Tasks.Task"/></returns>
   public abstract Task Execute(IExecutionContext<TArgs> context, CancellationToken cancellationToken);

   #endregion
}