// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Executable.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

internal class Executable<T> : IExecutable<T>
   where T : class
{
   #region Constructors and Destructors

   public Executable(T arguments, [NotNull] ICommandLineEngine commandLineEngine)
   {
      Arguments = arguments;
      CommandLineEngine = commandLineEngine ?? throw new ArgumentNullException(nameof(commandLineEngine));
      ExecutionEngine = CommandLineEngine.ExecutionEngine;
   }

   #endregion

   #region IExecutable<T> Members

   /// <summary>Gets the arguments.</summary>
   public T Arguments { get; private set; }

   public async Task<IExecutable<T>> RunAsync(string args, CancellationToken cancellationToken)
   {
      // TODO handle unmapped command line arguments
      // TODO support exception handling

      Arguments = CommandLineEngine.Map(args, Arguments);
      var executedCommand = await ExecutionEngine.ExecuteCommandAsync(Arguments, cancellationToken);
      if (executedCommand == null)
         await ExecutionEngine.ExecuteAsync(Arguments, cancellationToken);

      return this;
   }

   public async Task<IExecutable<T>> RunAsync(string[] args, CancellationToken cancellationToken)
   {
      // TODO handle unmapped command line arguments
      // TODO support exception handling

      Arguments = CommandLineEngine.Map(args, Arguments);
      var executedCommand = await ExecutionEngine.ExecuteCommandAsync(Arguments, cancellationToken);
      if (executedCommand == null)
         await ExecutionEngine.ExecuteAsync(Arguments, cancellationToken);

      return this;
   }

   #endregion

   #region Public Properties

   [NotNull] public ICommandLineEngine CommandLineEngine { get; }

   [NotNull] public IExecutionEngine ExecutionEngine { get; }

   #endregion
}