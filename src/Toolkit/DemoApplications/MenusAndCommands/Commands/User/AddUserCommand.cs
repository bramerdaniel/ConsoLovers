// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class AddUserCommand : ICommand<AddUserCommand.AddUserArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public AddUserCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<AddUserArgs> Members

   public AddUserArgs Arguments { get; set; }

   public void Execute()
   {
   }

   #endregion

   #region IMenuCommand Members

   public void ExecuteFromMenu(IMenuExecutionContext context)
   {
      console.WriteLine("User added");
      console.ReadLine();
   }

   #endregion

   public class AddUserArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")]
      public string Name { get; set; }

      #endregion
   }
}