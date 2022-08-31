// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsGroup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Settings;

using ConsoLovers.ConsoleToolkit.Core;

internal class SettingsGroup
{
   [MenuCommand("Add user")]
   public AddKnownUser AddUser { get; set; }

   [MenuCommand("show user")]
   public ShowKnownUsers ShowUsers { get; set; }
}