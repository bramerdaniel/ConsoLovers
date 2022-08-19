// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionCommands.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PermissionCommands
{
   #region Public Properties

   [Command("Add")]
   [ConsoleMenu("Remove permission")]
   public AddPermissionCommand Add { get; set; }

   [Command("Remove")]
   [ConsoleMenu("Remove", Hide = true)]
   public RemovePermissionCommand Remove { get; set; }

   #endregion
}