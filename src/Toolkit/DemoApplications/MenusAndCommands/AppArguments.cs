// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using MenusAndCommands.Commands;
using MenusAndCommands.Commands.Role;
using MenusAndCommands.Commands.User;

public class AppArguments
{
   #region Public Properties

   [Command("User")]
   public CommandGroup<UserCommands> User { get; set; }

   [Command("Roles")]
   public CommandGroup<RoleCommands> Role { get; set; }

   [Command("Execute")]
   public ExecuteCommand Execute { get; set; }

   #endregion
}