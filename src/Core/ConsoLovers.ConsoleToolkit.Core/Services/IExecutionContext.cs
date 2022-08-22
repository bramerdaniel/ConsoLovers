// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public interface IExecutionContext<T>
   where T : class
{
   #region Public Properties

   /// <summary>Gets or sets the application arguments.</summary>
   T ApplicationArguments { get; set; }

   /// <summary>Gets or sets the raw commandline (string or string[]).</summary>
   object Commandline { get; set; }

   /// <summary>Gets or sets the parsed arguments (normally set by the parser middleware).</summary>
   CommandLineArgumentList ParsedArguments { get; set; }

   #endregion
}