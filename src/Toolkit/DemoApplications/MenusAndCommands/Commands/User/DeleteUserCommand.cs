// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

public class DeleteUserCommand : ICommand<DeleteUserCommand.DeleteUserArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public DeleteUserCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<DeleteUserArgs> Members

   public DeleteUserArgs Arguments { get; set; }

   public void Execute()
   {
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      console.WriteLine("User deleted");
      console.ReadLine();
      context.MenuItem.Remove();
   }

   #endregion

   public class DeleteUserArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")] public string Name { get; set; }

      #endregion
   }
}