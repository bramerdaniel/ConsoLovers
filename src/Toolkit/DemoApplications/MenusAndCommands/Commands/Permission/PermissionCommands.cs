// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionCommands.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PermissionCommands
{
   #region Public Properties

   [Command("Add")]
   [MenuSettings("Add permission")]
   public AddPermissionCommand Add { get; set; }

   [Command("Remove")]
   [MenuSettings(Visible = false)]
   public RemovePermissionCommand Remove { get; set; }

   #endregion
}