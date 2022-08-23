// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowControllersCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ShowControllersCommand : ICommand<ShowControllerArgs>, IMenuCommand
{
   private readonly IConsole console;

   public ShowControllersCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #region ICommand<ShowControllerArgs> Members

   public void Execute()
   {
      console.WriteLine($"Showing controller of address {Arguments.Address}");
      console.ReadLine();
   }

   public ShowControllerArgs Arguments { get; set; }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      Execute();
   }

   #endregion
}

public class ShowControllerArgs
{
   #region Public Properties

   [Argument("address", Required = true)]
   [MenuCommand(Visible = true)]
   public string Address { get; set; } = "Huber";

   [Argument("force")]
   [MenuCommand(Visible = true)]
   public bool Force { get; set; }

   [Argument("hidden")]
   [Menu(false)]
   public bool Hidden { get; set; }

   #endregion
}