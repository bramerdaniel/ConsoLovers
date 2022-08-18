// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class RoleCommands
{
   [Command("Add")]
   public AddRoleCommand Add { get; set; }
}