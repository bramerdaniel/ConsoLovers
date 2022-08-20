// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Services;

using JetBrains.Annotations;

internal class ExecutionMiddleware<T> : Middleware<IExecutionContext<T>>
   where T : class
{
   #region Constants and Fields

   [NotNull]
   private readonly IExecutionEngine executionEngine;

   #endregion

   #region Constructors and Destructors

   public ExecutionMiddleware([NotNull] IExecutionEngine executionEngine)
   {
      this.executionEngine = executionEngine ?? throw new ArgumentNullException(nameof(executionEngine));
   }

   #endregion

   #region Public Methods and Operators

   public override async Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
         return;

      var executedCommand = await executionEngine.ExecuteCommandAsync(context.ApplicationArguments, context.CancellationToken);
      if (executedCommand == null)
         await executionEngine.ExecuteAsync(context.ApplicationArguments, context.CancellationToken);

      await Next(context, cancellationToken);
   }

   #endregion
}