// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Application.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Contracts;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Menu;

internal class Application : ConsoleApplication<AppArguments>
{
   private readonly CommandMenuManager menuManager;

   #region Constructors and Destructors

   public Application(ICommandLineEngine commandLineEngine, CommandMenuManager menuManager)
      : base(commandLineEngine)
   {
      this.menuManager = menuManager;
   }

   #endregion
}