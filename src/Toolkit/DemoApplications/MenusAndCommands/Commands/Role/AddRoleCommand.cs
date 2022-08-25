// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddRoleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;
using MenusAndCommands.Model;

public class AddRoleCommand : ICommand<AddRoleCommand.AddRoleArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IUserManager userManager;

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public AddRoleCommand(IUserManager userManager, IConsole console)
   {
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<AddRoleArgs> Members

   public AddRoleArgs Arguments { get; set; }

   public void Execute()
   {
      userManager.AddRole(Arguments.Name, Arguments.UserName, Arguments.UserName);
      console.WriteLine($"Role {Arguments.Name} was added successfully");
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      console.Clear();
      Execute();
      console.ReadLine();
   }

   #endregion

   public class AddRoleArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")]
      [MenuArgument("Role to delete")]
      public string Name { get; set; }

      #endregion
   }
}