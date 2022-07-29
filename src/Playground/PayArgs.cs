// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PayArgs
{
   [Command("test")]
   [HelpText("Normal command test")]
   public TestCommamd Test { get; set; } = null!;   
   
   [Command("async")]
   [HelpText("Async command test")]
   public AsyncTestCommamd AsyncTest{ get; set; } = null!;  
   
   [Command("help", "?")]
   [HelpText("Shows this help")]
   public HelpCommand Help{ get; set; } = null!;
}