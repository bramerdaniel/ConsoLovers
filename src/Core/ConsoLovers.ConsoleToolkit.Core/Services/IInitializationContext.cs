// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInitializationContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

internal interface IInitializationContext<T>
where T : class
{
   #region Public Properties

   T ApplicationArguments { get; set; }

   object Commandline { get; set; }

   CommandLineArgumentList ParsedArguments { get; set; }

   #endregion
}