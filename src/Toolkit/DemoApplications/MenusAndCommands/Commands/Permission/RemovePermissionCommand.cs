// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemovePermissionCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;

public class RemovePermissionCommand : IAsyncCommand
{
   #region IAsyncCommand Members

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }

   #endregion
}