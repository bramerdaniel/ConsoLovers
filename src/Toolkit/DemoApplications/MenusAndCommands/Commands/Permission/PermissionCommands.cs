// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using ConsoLovers.ConsoleToolkit.Core;

internal class PermissionCommands
{
   #region Public Properties

   [Command("Show")]
   [MenuCommand("Show permissions", ArgumentInitialization = ArgumentInitializationModes.Custom)]
   internal ShowPermissionCommand Show { get; set; }

   [Command("Add")]
   [MenuCommand("Add permission")]
   internal AddPermissionCommand Add { get; set; }

   [Command("Remove")]
   [MenuCommand("Remove permission", ArgumentInitialization = ArgumentInitializationModes.None)]
   internal RemovePermissionCommand Remove { get; set; }

   [Command("Remove")]
   [MenuCommand("Crash without initializer", ArgumentInitialization = ArgumentInitializationModes.Custom)]
   internal NoInitializerCommand Crash { get; set; }

   #endregion
}