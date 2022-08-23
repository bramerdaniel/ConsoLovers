// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using JetBrains.Annotations;

internal class ExecutionContext<T> : IExecutionContext<T>
   where T : class
{
   public CommandLineArgumentList ParsedArguments { get; set; }

   public T ApplicationArguments { get; set; }

   public object Commandline { get; set; }


   public ExecutionContext([NotNull] object commandLine)
   {
      Commandline = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
   }
}