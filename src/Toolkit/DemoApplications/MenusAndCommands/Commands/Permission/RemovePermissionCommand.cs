﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemovePermissionCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;

public class RemovePermissionCommand : IAsyncCommand
{
   private readonly IConsole console;

   public RemovePermissionCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #region IAsyncCommand Members

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      console.WriteLine("This command has no arguments and should work as expected", ConsoleColor.DarkYellow);
      console.WaitForEscapeOrNewline();
      return Task.CompletedTask;
   }

   #endregion
}