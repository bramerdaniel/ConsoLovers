// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using ConsoLovers.ConsoleToolkit.Core;

public class ControllerCommands
{
   #region Public Properties

   [Command("Show")]
   [MenuCommand("Show controllers")]
   public ShowControllersCommand Show { get; set; }

   #endregion

   #region Properties

   [Command("delete")]
   [MenuCommand("Delete controller", ArgumentInitializationMode = ArgumentInitializationModes.WhileExecution)]
   internal DeleteControllerCommand Delete { get; set; }

   #endregion
}