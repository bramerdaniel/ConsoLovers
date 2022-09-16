// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionResult.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Collections.Generic;

/// <summary>Default implementation of the <see cref="IExecutionResult"/></summary>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IExecutionResult"/>
internal class ExecutionResult : Dictionary<string, object>, IExecutionResult
{
   #region IExecutionResult Members

   /// <summary>Gets or sets the exit code.</summary>
   public int? ExitCode { get; set; }

   #endregion
}