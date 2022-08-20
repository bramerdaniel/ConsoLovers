// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>Interface defining a middleware component</summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
public interface IMiddleware<TContext>
{
   #region Public Properties

   /// <summary>Gets the next <see cref="Middleware{TContext}"/> delegate to invoke.</summary>
   Func<TContext, CancellationToken, Task> Next { get; set; }

   #endregion

   #region Public Methods and Operators

   /// <summary>The execution handler of the middleware.</summary>
   /// <param name="context">The context that is passed along the execution pipeline.</param>
   /// <param name="cancellationToken">The cancellation token for canceling the execution.</param>
   /// <returns>The execution <see cref="Task"/></returns>
   Task Execute(TContext context, CancellationToken cancellationToken);

   #endregion
}