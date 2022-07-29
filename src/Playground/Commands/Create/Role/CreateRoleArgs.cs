// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateRoleArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

namespace Playground.Commands.Create.Role;

public class CreateRoleArgs
{
   #region Public Properties

   [Argument("name", Index = 0)]
   [HelpText("Name of the role to create")]
   public string RoleName { get; set; } = null!;

   [Argument("rank", Index = 1)]
   [HelpText("Rank of the role to create")]
   public int Rank { get; set; }

   #endregion
}