// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ControllerCommands
{
   [Command("Show")]
   [MenuSettings("Show controllers")]
   public ShowControllersCommand Show { get; set; }
}