﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;

using Playground.Commands.Delete;

public class ApplicationArgs : TypeHelpProvider
{
   [Command("create")]
   [HelpText("Root command for nested command")]
   protected internal CreateCommand Create { get; set; } = null!;   
   
   [Command("delete")]
   [HelpText("Entry point for the delete stuff")]
   public CommandGroup<DeleteArgs> DeleteCommand { get; set; } = null!;     

   [Command("modify")]
   [HelpText("Async command test")]
   public ModifyCommamd AsyncTest{ get; set; } = null!;  
   
   [Command("help", "?")]
   [HelpText("Shows this help")]
   public HelpCommand Help{ get; set; } = null!;   

   [Option("w")]
   public bool Wait{ get; set; } = false;
   
   [Command("Fail")]
   public bool Fails{ get; set; } = false;

   public ApplicationArgs(IServiceProvider serviceProvider, ILocalizationService localizationService)
      : base(serviceProvider, localizationService)
   {
   }
}