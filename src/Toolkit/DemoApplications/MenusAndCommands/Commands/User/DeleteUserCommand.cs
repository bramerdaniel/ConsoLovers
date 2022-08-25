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
      console.WriteLine($"User {Arguments.Name} was deleted");
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      Execute();
      console.ReadLine();
      context.MenuItem.Remove();
   }

   #endregion

   public class DeleteUserArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")]
      [MenuArgument("User to delete")]
      public string Name { get; set; }

      #endregion
   }
}