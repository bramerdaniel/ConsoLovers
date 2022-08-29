// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveRoleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

using MenusAndCommands.Model;

public class RemoveRoleCommand : ICommand<RemoveRoleCommand.RemoveRoleArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   private readonly IUserManager userManager;

   #endregion

   #region Constructors and Destructors

   public RemoveRoleCommand(IUserManager userManager, IConsole console)
   {
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<RemoveRoleArgs> Members

   public RemoveRoleArgs Arguments { get; set; }

   public void Execute()
   {
      userManager.DeleteRole(Arguments.Name, Arguments.UserName, Arguments.Password);
      console.WriteLine($"Role {Arguments.Name} was removed ({Arguments.Force})");
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      Execute();
      console.ReadLine();
   }

   #endregion

   public class RemoveRoleArgs : SharedArgs
   {
      #region Public Properties

      [Argument("force", "f")]
      [MenuArgument(Visibility = ArgumentVisibility.Hidden)]
      public bool Force { get; set; }

      [Argument("name", Required = true)]
      [MenuArgument(DisplayName = "Role name", DisplayOrder = 1)]
      public string Name { get; set; }

      #endregion
   }
}