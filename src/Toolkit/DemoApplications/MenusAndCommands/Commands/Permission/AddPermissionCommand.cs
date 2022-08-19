// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPermissionCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class AddPermissionCommand : IAsyncCommand
{
   private readonly IConsole console;

   public AddPermissionCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      console.WriteLine("Permission was added !", ConsoleColor.DarkYellow);
      console.ReadLine();
      return Task.CompletedTask;
   }
}