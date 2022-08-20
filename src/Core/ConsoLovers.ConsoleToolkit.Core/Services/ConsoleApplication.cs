﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.Builders;

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

internal class ConsoleApplication<T> : IConsoleApplication<T>
   where T : class
{
   #region Constructors and Destructors

   public ConsoleApplication(T arguments, [NotNull] ICommandLineEngine commandLineEngine)
   {
      Arguments = arguments;
      CommandLineEngine = commandLineEngine ?? throw new ArgumentNullException(nameof(commandLineEngine));
      ExecutionEngine = CommandLineEngine.ExecutionEngine;
   }

   #endregion

   #region IConsoleApplication<T> Members

   /// <summary>Gets the arguments.</summary>
   public T Arguments { get; private set; }

   public async Task<IConsoleApplication<T>> RunAsync(string args, CancellationToken cancellationToken)
   {
      // TODO handle unmapped command line arguments
      // TODO support exception handling
      // TODO support IArgumentInitializer<> again ?

      Arguments = CommandLineEngine.Map(args, Arguments);
      return await RunInternalAsync(cancellationToken);
   }

   public async Task<IConsoleApplication<T>> RunAsync(string[] args, CancellationToken cancellationToken)
   {
      // TODO handle unmapped command line arguments
      // TODO support exception handling
      // TODO support IArgumentInitializer<> again ?

      Arguments = CommandLineEngine.Map(args, Arguments);
      return await RunInternalAsync(cancellationToken);
   }

   private async Task<IConsoleApplication<T>> RunInternalAsync(CancellationToken cancellationToken)
   {
      var executedCommand = await ExecutionEngine.ExecuteCommandAsync(Arguments, cancellationToken);
      if (executedCommand == null)
         await ExecutionEngine.ExecuteAsync(Arguments, cancellationToken);
      return this;
   }

   #endregion

   #region Public Properties

   [NotNull] private ICommandLineEngine CommandLineEngine { get; }

   [NotNull] private IExecutionEngine ExecutionEngine { get; }

   #endregion
}