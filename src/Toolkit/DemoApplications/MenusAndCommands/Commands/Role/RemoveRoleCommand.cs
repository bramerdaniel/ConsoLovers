// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveRoleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using System;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class RemoveRoleCommand : ICommand<RemoveRoleCommand.RemoveRoleArgs>, IMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public RemoveRoleCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region ICommand<AddRoleArgs> Members

   public RemoveRoleArgs Arguments { get; set; }

   public void Execute()
   {
      if (Arguments == null)
      {
         console.WriteLine("Arguments were null", ConsoleColor.Red);
         console.ReadLine();
         return;
      }

      console.WriteLine($"Role {Arguments.Name} was removed ({Arguments.Force})");
      console.ReadLine();
   }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      Execute();
   }

   #endregion

   public class RemoveRoleArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")]
      public string Name { get; set; }

      [Argument("force", "f")]
      public bool Force { get; set; }

      #endregion
   }

}