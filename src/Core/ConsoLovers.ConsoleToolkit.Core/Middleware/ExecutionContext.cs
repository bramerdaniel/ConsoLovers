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

   public ExecutionContext([NotNull] object commandLine, [NotNull] IExecutionResult result)
   {
      Commandline = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
      Result = result ?? throw new ArgumentNullException(nameof(result));
   }

   #endregion

   #region IExecutionContext<T> Members

   /// <summary>Gets or sets the parsed arguments (normally set by the parser middleware).</summary>
   public ICommandLineArguments ParsedArguments { get; set; }

   /// <summary>Gets the result of the execution.</summary>
   public IExecutionResult Result { get; }

   /// <summary>Gets or sets the application arguments.</summary>
   public T ApplicationArguments { get; set; }

   /// <summary>Gets or sets the raw commandline (string or string[]).</summary>
   public object Commandline { get; set; }

   #endregion
}