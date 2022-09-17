// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.Middleware;

using JetBrains.Annotations;

/// <summary>Entry point for creating an <see cref="IConsoleApplication{T}"/></summary>
public static class ConsoleApplication
{
   #region Public Methods and Operators

   /// <summary>Creates an <see cref="IApplicationBuilder{T}"/></summary>
   /// <typeparam name="TArguments">The type of the arguments the application will use.</typeparam>
   /// <returns>The created <see cref="IApplicationBuilder{T}"/></returns>
   public static IApplicationBuilder<TArguments> WithArguments<TArguments>()
      where TArguments : class
   {
      return new ApplicationBuilder<TArguments>();
   }

   #endregion
}

/// <summary>
///    The default implementation of the <see cref="IConsoleApplication{T}"/> interface. This is where all the executionEngine workflow is
///    orchestrated
/// </summary>
/// <typeparam name="T">The argument type of the application</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.Builders.IConsoleApplication&lt;T&gt;"/>
internal class ConsoleApplication<T> : IConsoleApplication<T>
   where T : class
{
   #region Constants and Fields

   private readonly IExecutionPipeline<T> executionPipeline;

   private ExecutionContext<T> context;

   #endregion

   #region Constructors and Destructors

   public ConsoleApplication([NotNull] IExecutionPipeline<T> executionPipeline, IExecutionResult result)
   {
      this.executionPipeline = executionPipeline ?? throw new ArgumentNullException(nameof(executionPipeline));
      Result = result;
   }

   #endregion

   #region IConsoleApplication<T> Members

   /// <summary>Gets the arguments.</summary>
   public T Arguments => context?.ApplicationArguments;

   /// <summary>Gets the result of the execution.</summary>
   public IExecutionResult Result { get; }

   public async Task<IConsoleApplication<T>> RunAsync(string args, CancellationToken cancellationToken)
   {
      await ExecutePipelineAsync(args, cancellationToken);
      return this;
   }

   public async Task<IConsoleApplication<T>> RunAsync(string[] args, CancellationToken cancellationToken)
   {
      await ExecutePipelineAsync(args, cancellationToken);
      return this;
   }

   #endregion

   #region Methods

   private async Task ExecuteInternalAsync(object args, CancellationToken cancellationToken)
   {
      context = new ExecutionContext<T>(args, Result);
      await executionPipeline.ExecuteAsync(context, cancellationToken);
   }

   private Task ExecutePipelineAsync([NotNull] object args, CancellationToken cancellationToken)
   {
      if (args == null)
         throw new ArgumentNullException(nameof(args));

      return ExecuteInternalAsync(args, cancellationToken);
   }

   #endregion
}