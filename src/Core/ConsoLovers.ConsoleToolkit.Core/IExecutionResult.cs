// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionResult.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Collections.Generic;

/// <summary>The result of an execution</summary>
public interface IExecutionResult : IDictionary<string, object>
{
   #region Public Properties

   /// <summary>Gets or sets the exit code.</summary>
   public int? ExitCode { get; set; }

   #endregion
}