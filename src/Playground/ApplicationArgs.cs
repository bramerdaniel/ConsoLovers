// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using Playground.Commands.Delete;

public class ApplicationArgs
{
   [Command("create")]
   [HelpText("Root command for nested command")]
   public CreateCommand Create { get; set; } = null!;   
   
   [Command("delete")]
   [HelpText("Entry point for the delete stuff")]
   public DeleteCommandProxy DeleteCommand { get; set; } = null!;     

   [Command("async")]
   [HelpText("Async command test")]
   public AsyncTestCommamd AsyncTest{ get; set; } = null!;  
   
   [Command("help", "?")]
   [HelpText("Shows this help")]
   public HelpCommand Help{ get; set; } = null!;
}