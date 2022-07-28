// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PayArgs
{
   [Command("test")]
   public TestCommamd Test { get; set; } = null!;
}