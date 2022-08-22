// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Services;

using JetBrains.Annotations;

/// <summary>The middleware that is responsible for executing the defined commands or application logic</summary>
/// <typeparam name="T">The type of the arguments</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.Middleware.Middleware&lt;ConsoLovers.ConsoleToolkit.Core.Services.IExecutionContext&lt;T&gt;&gt;"/>
public class ExecutionMiddleware<T> : Middleware<T>
   where T : class
{
   #region Constants and Fields

   [NotNull]
   private readonly IExecutionEngine executionEngine;

   #endregion

   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="ExecutionMiddleware{T}"/> class.</summary>
   /// <param name="executionEngine">The execution engine.</param>
   /// <exception cref="System.ArgumentNullException">executionEngine</exception>
   public ExecutionMiddleware([NotNull] IExecutionEngine executionEngine)
   {
      this.executionEngine = executionEngine ?? throw new ArgumentNullException(nameof(executionEngine));
   }

   #endregion

   #region Public Properties

   public override int ExecutionOrder => KnownLocations.ExecutionMiddleware;

   #endregion

   #region Public Methods and Operators

   public override async Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
         return;

      var executedCommand = await executionEngine.ExecuteCommandAsync(context.ApplicationArguments, cancellationToken);
      if (executedCommand == null)
         await executionEngine.ExecuteAsync(context.ApplicationArguments, cancellationToken);

      await Next(context, cancellationToken);
   }

   #endregion
}