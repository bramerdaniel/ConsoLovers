// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HiddenCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands;

using ConsoLovers.ConsoleToolkit.Core;

using global::MenusAndCommands.Commands.Permission;
using global::MenusAndCommands.Commands.Role;

public class HiddenCommands
{
   #region Public Properties

   [Command("Add")] 
   public AddRoleCommand Add { get; set; }

   [Command("Remove")] 
   public RemovePermissionCommand Remove { get; set; }

   #endregion
}