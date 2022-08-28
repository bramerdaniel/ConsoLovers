// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowControllersCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using System;

using ConsoLovers.ConsoleToolkit.Core;

public class ShowControllersCommand : ICommand<ShowControllerArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public ShowControllersCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<ShowControllerArgs> Members

   public void Execute()
   {
      if (Arguments == null)
      {
         console.WriteLine("Arguments were null", ConsoleColor.Red);
         console.ReadLine();
         return;
      }

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
   [MenuArgument(VisibleInMenu = true)]
   public string Address { get; set; } = "Huber";

   [Argument("force")]
   [MenuArgument(VisibleInMenu = true)]
   public bool Force { get; set; }

   [Argument("hidden")]
   [MenuArgument(VisibleInMenu = false)]
   public bool Hidden { get; set; }

   #endregion
}