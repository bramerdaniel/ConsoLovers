// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public class CommandLineOptions : ICommandLineOptions
{
   #region ICommandLineOptions Members

   public bool CaseSensitive { get; set; }

   #endregion
}