// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteRoleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.Role;

using ConsoLovers.ConsoleToolkit.Core;

public class DeleteRoleCommand : IAsyncCommand<DeleteRoleArgs>
{
   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      Console.WriteLine($"Role {Arguments.RoleName} was delete");
      return Task.CompletedTask;
   }

   public DeleteRoleArgs Arguments { get; set; } = null!;
}