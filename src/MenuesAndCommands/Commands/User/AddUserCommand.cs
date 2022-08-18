// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class AddUserCommand : ICommand<AddUserCommand.AddUserArgs>, IMenuCommand
{
   private readonly IConsole console;

   #region ICommand<AddUserArgs> Members

   public AddUserCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public AddUserArgs Arguments { get; set; }

   public void Execute()
   {
   }

   #endregion

   public class AddUserArgs
   {
      #region Public Properties

      [Argument("name")] public string Name { get; set; }

      #endregion
   }

   public void ExecuteFromMenu()
   {
      console.WriteLine("User added");
   }
}