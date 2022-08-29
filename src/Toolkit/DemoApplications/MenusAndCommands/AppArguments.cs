﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using ConsoLovers.ConsoleToolkit.Core;

using global::MenusAndCommands.Commands.Controllers;

using MenusAndCommands.Commands;
using MenusAndCommands.Commands.Permission;
using MenusAndCommands.Commands.Role;
using MenusAndCommands.Commands.User;

public class AppArguments
{
   #region Public Properties

   [Command("Controller")]
   [MenuCommand("Manage controllers")]
   public CommandGroup<ControllerCommands> Controller { get; set; }

   //[Command("User")]
   //[MenuCommand("Manage users")]
   //public CommandGroup<UserCommands> User { get; set; }

   //[Command("Roles")]
   //[MenuCommand("Manage roles")]
   //public CommandGroup<RoleCommands> Role { get; set; }

   //[Command("Permissions")]
   //public CommandGroup<PermissionCommands> Permissions { get; set; }

   //[Command("Hidden")]
   //[MenuCommand(Visibility = CommandVisibility.Hidden)]
   //public CommandGroup<HiddenCommands> Hidden { get; set; }

   //[Command("Clear")]
   //public ClearCommand Clear { get; set; }

   //[Command("help", "?")]
   //[MenuCommand(Visibility = CommandVisibility.Hidden)]
   //public HelpCommand Help { get; set; }

   #endregion
}