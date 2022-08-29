// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using ConsoLovers.ConsoleToolkit.Core;

public class RoleCommands
{
   #region Public Properties

   [Command("Show")]
   [MenuCommand("Show", ArgumentInitialization = ArgumentInitializationModes.Custom)]
   public ShowRolesCommand Show { get; set; }

   [Command("Add")]
   [MenuCommand("Add role")]
   public AddRoleCommand Add { get; set; }

   [Command("Remove")]
   [MenuCommand("Remove role")]
   public RemoveRoleCommand Remove { get; set; }

   #endregion
}