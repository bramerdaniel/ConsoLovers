﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using MenusAndCommands.Commands;
using MenusAndCommands.Commands.Permission;
using MenusAndCommands.Commands.Role;
using MenusAndCommands.Commands.User;

public class AppArguments
{
   #region Public Properties

   [Command("User")]
   [MenuSettings("Manage users")]
   public CommandGroup<UserCommands> User { get; set; }

   [Command("Roles")]
   [MenuSettings("Manage roles")]
   public CommandGroup<RoleCommands> Role { get; set; }

   [Command("Permissions")]
   public CommandGroup<PermissionCommands> Permissions { get; set; }

   [Command("Hidden")]
   public CommandGroup<HiddenCommands> Hidden { get; set; }

   [Command("Execute")]
   public ExecuteCommand Execute { get; set; }

   [Command("help", "?")]
   public HelpCommand Help{ get; set; }

   #endregion
}