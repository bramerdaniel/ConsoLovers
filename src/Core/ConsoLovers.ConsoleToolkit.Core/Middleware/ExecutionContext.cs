// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;

using JetBrains.Annotations;

internal class ExecutionContext<T> : IExecutionContext<T>
   where T : class
{
   #region Constructors and Destructors

   public ExecutionContext([NotNull] object commandLine)
   {
      Commandline = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
   }

   #endregion

   #region IExecutionContext<T> Members

   public ICommandLineArguments ParsedArguments { get; set; }

   public T ApplicationArguments { get; set; }

   public object Commandline { get; set; }

   #endregion
}