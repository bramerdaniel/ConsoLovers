// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
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

   public ConsoleApplication([NotNull] IExecutionPipeline<T> executionPipeline)
   {
      // Arguments = arguments;
      this.executionPipeline = executionPipeline ?? throw new ArgumentNullException(nameof(executionPipeline));
   }

   #endregion

   #region IConsoleApplication<T> Members

   /// <summary>Gets the arguments.</summary>
   public T Arguments => context?.ApplicationArguments;

   public async Task<IConsoleApplication<T>> RunAsync(string args, CancellationToken cancellationToken)
   {
      await ExecutePipelineAnsyc(args, cancellationToken);
      return this;
   }

   public async Task<IConsoleApplication<T>> RunAsync(string[] args, CancellationToken cancellationToken)
   {
      await ExecutePipelineAnsyc(args, cancellationToken);
      return this;
   }

   #endregion

   #region Methods

   private async Task ExecutePipelineAnsyc([NotNull] object args, CancellationToken cancellationToken)
   {
      if (args == null)
         throw new ArgumentNullException(nameof(args));


      context = new ExecutionContext<T>(args);
      await executionPipeline.Execute(context, cancellationToken);
   }

   #endregion
}