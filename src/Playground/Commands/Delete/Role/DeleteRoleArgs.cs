// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteRoleArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.Role;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class DeleteRoleArgs
{
   [Argument("name", Index = 0)]
   [HelpText("Name of the role to delete")]
   public string RoleName { get; set; } = null!;
}