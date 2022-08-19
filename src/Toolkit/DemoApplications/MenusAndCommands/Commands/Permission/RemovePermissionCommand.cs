// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemovePermissionCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.CommandExtensions;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;


public class RemovePermissionCommand : IAsyncCommand
{
   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }
}