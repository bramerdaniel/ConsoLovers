// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class RoleCommands
{
   #region Public Properties

   [Command("Add")]
   public AddRoleCommand Add { get; set; }

   [Command("Remove")]
   [MenuCommand("Remove role", InitMode = InitModes.None)]
   public RemoveRoleCommand Remove { get; set; }

   #endregion
}