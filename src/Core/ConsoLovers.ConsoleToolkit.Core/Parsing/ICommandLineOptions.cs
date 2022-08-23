// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineOptions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public interface ICommandLineOptions
{
   /// <summary>Gets or sets a value indicating whether the parser is case sensitive.</summary>
   bool CaseSensitive { get; set; }
}