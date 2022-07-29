// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModifyCommamd.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ModifyCommamd : IAsyncCommand<CreateArgs>
{
   public CreateArgs Arguments { get; set; }

   public Task ExecuteAsync()
   {
      
      return Task.CompletedTask;
   }
}