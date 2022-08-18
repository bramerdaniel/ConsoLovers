// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class UserCommands
{
   [Command("Add")]
   public AddUserCommand Add { get; set; }

   [Command("Delete")]
   public DeleteUserCommand Delete { get; set; }
}