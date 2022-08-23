// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HiddenCommands.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using MenusAndCommands.Commands.Permission;
using MenusAndCommands.Commands.Role;

[MenuSettings(Visible = false)]
public class HiddenCommands
{
   [Command("Add")]
   public AddRoleCommand Add { get; set; }

   [Command("Remove")]
   public RemovePermissionCommand Remove { get; set; }
}