// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddRoleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

public class AddRoleCommand : ICommand<AddRoleCommand.AddRoleArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public AddRoleCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<AddRoleArgs> Members

   public AddRoleArgs Arguments { get; set; }

   public void Execute()
   {
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      console.WriteLine("Role added");
      console.ReadLine();
   }

   #endregion

   public class AddRoleArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")] public string Name { get; set; }

      #endregion
   }
}