// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

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

   public void Execute(IMenuExecutionContext context)
   {
      console.WriteLine("User added");
      console.ReadLine();
   }

   #endregion

   public class AddUserArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")] public string Name { get; set; }

      #endregion
   }
}