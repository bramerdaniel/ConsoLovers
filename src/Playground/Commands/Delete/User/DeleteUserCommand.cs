// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.User;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class DeleteUserCommand : IAsyncCommand<DeleteUserArgs>
{
   #region IAsyncCommand<DeleteUserArgs> Members

   public Task ExecuteAsync()
   {
      Console.WriteLine($"User {Arguments.UserName} was delete. Forced was {Arguments.Force}");
      return Task.CompletedTask;
   }

   public DeleteUserArgs Arguments { get; set; }

   #endregion
}