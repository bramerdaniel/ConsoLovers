// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public interface ICommandLineOptions
{
   #region Public Properties

   /// <summary>Gets or sets a value indicating whether the parser is case sensitive.</summary>
   bool CaseSensitive { get; set; }

   #endregion
}