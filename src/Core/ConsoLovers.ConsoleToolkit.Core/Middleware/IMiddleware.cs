// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Interface defining a middleware component</summary>
/// <typeparam name="T">The type of the application arguments.</typeparam>
public interface IMiddleware<T>
   where T : class
{
   #region Public Properties

   int ExecutionOrder { get; }

   /// <summary>Gets the next <see cref="Middleware{TContext}"/> delegate to invoke.</summary>
   Func<IExecutionContext<T>, CancellationToken, Task> Next { get; set; }

   #endregion

   #region Public Methods and Operators

   /// <summary>The execution handler of the middleware.</summary>
   /// <param name="context">The context that is passed along the execution pipeline.</param>
   /// <param name="cancellationToken">The cancellation token for canceling the execution.</param>
   /// <returns>The execution <see cref="Task"/></returns>
   Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken);

   #endregion
}