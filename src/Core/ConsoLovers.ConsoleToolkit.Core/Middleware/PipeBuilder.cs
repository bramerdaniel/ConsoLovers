// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PipeBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

public class PipeBuilder<T>
{
   #region Constants and Fields

   readonly Func<T, CancellationToken, Task> finalStep;

   
   private readonly IList<IMiddleware<T>> middlewares;

   #endregion

   #region Constructors and Destructors

   public PipeBuilder(Func<T, CancellationToken, Task> finalStep)
   {
      this.finalStep = finalStep;

      middlewares= new List<IMiddleware<T>>();
   }

   public PipeBuilder()
      : this(FinalStep)
   {
   }

   #endregion

   #region Public Methods and Operators

   public PipeBuilder<T> AddMiddleware(IMiddleware<T> middleware)
   {
      middlewares.Add(middleware);
      return this;
   }

   public Func<T, CancellationToken, Task> Build()
   {
      return CreatePipe();
   }

   #endregion

   #region Methods

   /// <summary>The finals the step of the middleware pipeline.</summary>
   /// <param name="context">The context that was passed.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns></returns>
   private static Task FinalStep(T context, CancellationToken cancellationToken)
   {
      return cancellationToken.IsCancellationRequested ? Task.FromCanceled(cancellationToken) : Task.CompletedTask;
   }

   private Func<T, CancellationToken, Task> CreatePipe()
   {
      var reversed = middlewares.Reverse().ToArray();
      var current = reversed[0];
      current.Next = finalStep;

      foreach (var middleware in reversed.Skip(1))
      {
         middleware.Next = current.Execute;
         current = middleware;
      }

      return current.Execute;
   }

   #endregion
}