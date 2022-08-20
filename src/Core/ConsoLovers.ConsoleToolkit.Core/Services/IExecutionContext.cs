// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System.Threading;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public interface IExecutionContext<T>
where T : class
{
   #region Public Properties

   T ApplicationArguments { get; set; }

   CancellationToken CancellationToken { get; }

   object Commandline { get; set; }

   CommandLineArgumentList ParsedArguments { get; set; }

   #endregion
}