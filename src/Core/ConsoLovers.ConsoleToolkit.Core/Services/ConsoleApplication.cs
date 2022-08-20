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
   private readonly IInitializationPipeline initializationPipeline;


   #region Constructors and Destructors

   public ConsoleApplication(T arguments, [NotNull] ICommandLineEngine commandLineEngine, [NotNull] IExecutionEngine executionEngine,
      [NotNull] IInitializationPipeline initializationPipeline)
   {
      this.initializationPipeline = initializationPipeline ?? throw new ArgumentNullException(nameof(initializationPipeline));
      Arguments = arguments;
      CommandLineEngine = commandLineEngine ?? throw new ArgumentNullException(nameof(commandLineEngine));
      ExecutionEngine = executionEngine ?? throw new ArgumentNullException(nameof(executionEngine));
   }

   #endregion

   #region IConsoleApplication<T> Members

   /// <summary>Gets the arguments.</summary>
   public T Arguments { get; private set; }

   public async Task<IConsoleApplication<T>> RunAsync(string args, CancellationToken cancellationToken)
   {
      // TODO handle unmapped command line arguments
      // TODO support exception handling

      InitializeArguments(args);
      return await RunInternalAsync(cancellationToken);

   }

   public async Task<IConsoleApplication<T>> RunAsync(string[] args, CancellationToken cancellationToken)
   {
      // TODO handle unmapped command line arguments
      // TODO support exception handling

      InitializeArguments(args);
      return await RunInternalAsync(cancellationToken);
   }

   #endregion

   #region Properties

   [NotNull] private ICommandLineEngine CommandLineEngine { get; }

   [NotNull] private IExecutionEngine ExecutionEngine { get; }

   #endregion

   #region Methods

   private bool Handle(Exception exception)
   {
      try
      {
         return true;
      }
      catch (Exception e)
      {
         return false;
      }
   }

   private void InitializeArguments([NotNull] object args)
   {
      if (args == null)
         throw new ArgumentNullException(nameof(args));

      // TODO support IArgumentInitializer<> again ?
      initializationPipeline.Execute(new InitializationContext<T>(Arguments, args));
   }

   private async Task<IConsoleApplication<T>> RunInternalAsync(CancellationToken cancellationToken)
   {
      var executedCommand = await ExecutionEngine.ExecuteCommandAsync(Arguments, cancellationToken);
      if (executedCommand == null)
         await ExecutionEngine.ExecuteAsync(Arguments, cancellationToken);
      return this;
   }

   #endregion
}