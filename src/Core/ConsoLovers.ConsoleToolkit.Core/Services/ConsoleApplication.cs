// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

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

   #endregion

   #region Constructors and Destructors

   public ConsoleApplication(T arguments, [NotNull] IExecutionEngine executionEngine, [NotNull] IExecutionPipeline<T> executionPipeline)
   {
      this.executionPipeline = executionPipeline ?? throw new ArgumentNullException(nameof(executionPipeline));
      Arguments = arguments;
      ExecutionEngine = executionEngine ?? throw new ArgumentNullException(nameof(executionEngine));
   }

   #endregion

   #region IConsoleApplication<T> Members

   /// <summary>Gets the arguments.</summary>
   public T Arguments { get; private set; }

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

   #region Properties

   [NotNull] private IExecutionEngine ExecutionEngine { get; }

   #endregion

   #region Methods

   private async Task ExecutePipelineAnsyc([NotNull] object args, CancellationToken cancellationToken)
   {
      if (args == null)
         throw new ArgumentNullException(nameof(args));

      // TODO handle unmapped command line arguments
      // TODO support exception handling

      var context = new ExecutionContext<T>(Arguments, args);
      await executionPipeline.Execute(context, cancellationToken);
   }


   #endregion
}