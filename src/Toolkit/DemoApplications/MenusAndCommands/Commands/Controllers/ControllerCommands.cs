// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using ConsoLovers.ConsoleToolkit.Core;

public class ControllerCommands
{
   #region Properties

   [Command("delete")]
   [MenuCommand("Delete controller", ArgumentInitialization = ArgumentInitializationModes.WhileExecution)]
   internal DeleteControllerCommand Delete { get; set; }

   [Command("deleteMenu")]
   [MenuCommand("Delete with menu", DisplayOrder = 1, ArgumentInitialization = ArgumentInitializationModes.AsMenu)]
   internal DeleteControllerCommand DeleteMenu { get; set; }

   [Command("Show")]
   [MenuCommand("Show controllers")]
   internal ShowControllersCommand Show { get; set; }

   #endregion
}