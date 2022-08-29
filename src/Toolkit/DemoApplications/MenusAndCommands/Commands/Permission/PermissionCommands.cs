// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using ConsoLovers.ConsoleToolkit.Core;

public class PermissionCommands
{
   #region Public Properties

   [Command("Add")]
   [MenuCommand("Add permission")]
   public AddPermissionCommand Add { get; set; }

   [Command("Remove")]
   [MenuCommand(ArgumentInitializationMode = ArgumentInitializationModes.Custom)]
   public RemovePermissionCommand Remove { get; set; }

   #endregion
}