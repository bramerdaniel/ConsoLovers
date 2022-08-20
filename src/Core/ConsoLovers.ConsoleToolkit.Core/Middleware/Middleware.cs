// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Middleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;

using JetBrains.Annotations;

public abstract class Middleware<TContext>
{
   /// <summary>Gets the <see cref="Middleware{TContext}"/> to invoke.</summary>
   public Action<TContext> Next { get; set; }

   protected Middleware([NotNull] Action<TContext> handler)
   {
      Next = handler ?? throw new ArgumentNullException(nameof(handler));
   }
   protected Middleware()
   {
   }

   public abstract void Execute(TContext context);
}