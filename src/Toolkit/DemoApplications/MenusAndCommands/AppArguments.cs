// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using global::MenusAndCommands.Commands;
using global::MenusAndCommands.Commands.Controllers;
using global::MenusAndCommands.Commands.Permission;
using global::MenusAndCommands.Commands.Role;
using global::MenusAndCommands.Commands.User;

public class AppArguments
{
   #region Public Properties

   [Command("Controller")]
   [MenuCommand("Manage controllers")]
   public CommandGroup<ControllerCommands> Controller{ get; set; }

   [Command("User")]
   [MenuCommand("Manage users")]
   public CommandGroup<UserCommands> User { get; set; }

   [Command("Roles")]
   [MenuCommand("Manage roles")]
   public CommandGroup<RoleCommands> Role { get; set; }

   [Command("Permissions")]
   public CommandGroup<PermissionCommands> Permissions { get; set; }

   [Command("Hidden")]
   public CommandGroup<HiddenCommands> Hidden { get; set; }

   [Command("Clear")]
   public ClearCommand Clear { get; set; }

   [Command("help", "?")]
   [Menu(Visible = false)]
   public HelpCommand Help{ get; set; }

   #endregion
}