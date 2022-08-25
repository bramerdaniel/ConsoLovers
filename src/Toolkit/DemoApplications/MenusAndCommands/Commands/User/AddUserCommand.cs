// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

using MenusAndCommands.Model;

public class AddUserCommand : ICommand<AddUserCommand.AddUserArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IUserManager userManager;

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public AddUserCommand(IUserManager userManager, IConsole console)
   {
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<AddUserArgs> Members

   public AddUserArgs Arguments { get; set; }

   public void Execute()
   {
      var user = new User{ Name = Arguments.Name, Password = Arguments.Name };
      userManager.AddUser(user, Arguments.UserName, Arguments.Password);

      console.WriteLine($"User {Arguments.Name} was added");
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      Execute();
      console.ReadLine();
   }

   #endregion

   public class AddUserArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")]
      [MenuArgument("Name to add", DisplayOrder = 1)]
      public string Name { get; set; }

      #endregion
   }
}