// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPermissionCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;

public class AddPermissionCommand : IAsyncCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public AddPermissionCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region IAsyncCommand Members

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      console.WriteLine("Permission was added !", ConsoleColor.DarkYellow);
      console.ReadLine();
      return Task.CompletedTask;
   }

   #endregion
}