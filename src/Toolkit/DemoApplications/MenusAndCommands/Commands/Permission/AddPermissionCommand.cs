// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPermissionCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class AddPermissionCommand : IAsyncCommand
{
   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }
}