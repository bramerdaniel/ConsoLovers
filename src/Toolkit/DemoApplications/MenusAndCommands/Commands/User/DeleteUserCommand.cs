// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;

using MenusAndCommands.Model;

public class DeleteUserCommand : ICommand<DeleteUserCommand.DeleteUserArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   private readonly IUserManager userManager;

   #endregion

   #region Constructors and Destructors

   public DeleteUserCommand(IConsole console, IUserManager userManager)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
   }

   #endregion

   #region ICommand<DeleteUserArgs> Members

   public DeleteUserArgs Arguments { get; set; }

   public void Execute()
   {
      var user = userManager.GetUsers(Arguments.UserName, Arguments.Password)
         .FirstOrDefault(x => x.Name == Arguments.Name);

      if (user == null)
         throw new InvalidOperationException($"User {Arguments.Name} does not exist.");

      userManager.DeleteUser(user, Arguments.UserName, Arguments.Password);
      console.WriteLine($"User {Arguments.Name} was deleted");
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      Execute();
      console.ReadLine();
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