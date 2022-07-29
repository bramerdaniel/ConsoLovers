// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class RootCommand<T> : ICommand<T>
{
   public void Execute()
   {
      CommandExecutor.ExecuteCommandAsync<T>(Arguments).GetAwaiter().GetResult();
   }

   public T Arguments { get; set; }
}