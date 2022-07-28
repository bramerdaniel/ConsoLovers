// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class TestArgs
{
   #region Public Properties

   [Argument("n", Index = 0)]
   public int Number { get; set; }

   #endregion
}