// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using ConsoLovers.ConsoleToolkit.Core;

using global::MenusAndCommands.Commands.Controllers;

public class AppArguments
{
   #region Public Properties

   [Command("Show")]
   [MenuCommand("Show controllers", ArgumentInitializationMode = ArgumentInitializationModes.AsMenu)]
   internal ShowControllersCommand Show { get; set; }

   //[Command("Controller")]
   //[MenuCommand("Manage controllers")]
   //public CommandGroup<ControllerCommands> Controller { get; set; }

   //[Command("User")]
   //[MenuCommand("Manage users")]
   //public CommandGroup<UserCommands> User { get; set; }

   //[Command("Roles")]
   //[MenuCommand("Manage roles")]
   //public CommandGroup<RoleCommands> Role { get; set; }

   //[Command("Permissions")]
   //public CommandGroup<PermissionCommands> Permissions { get; set; }

   //[Command("Hidden")]
   //[MenuCommand(VisibleInMenu = false)]
   //public CommandGroup<HiddenCommands> Hidden { get; set; }

   //[Command("Clear")]
   //public ClearCommand Clear { get; set; }

   //[Command("help", "?")]
   //[MenuCommand(VisibleInMenu = false)]
   //public HelpCommand Help{ get; set; }

   #endregion
}