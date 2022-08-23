// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowControllersCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ShowControllersCommand : ICommand<ShowControllerArgs>, IMenuCommand
{
   #region ICommand<ShowControllerArgs> Members

   public void Execute()
   {
   }

   public ShowControllerArgs Arguments { get; set; }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
   }

   #endregion
}

public class ShowControllerArgs
{
   #region Public Properties

   [Argument("address")]
   [MenuSettings(Visible = true)]
   public string Address { get; set; }

   #endregion
}