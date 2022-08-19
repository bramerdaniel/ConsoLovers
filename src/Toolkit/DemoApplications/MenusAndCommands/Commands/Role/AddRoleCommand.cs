// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddRoleCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using System;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

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

   public void ExecuteFromMenu()
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