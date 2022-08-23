// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteControllerCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;

internal class DeleteControllerCommand : IAsyncCommand<DeleteControllerCommand.Args>
{
   private readonly IConsole console;

   #region IAsyncCommand<Args> Members

   public DeleteControllerCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      if (Arguments == null)
      {
         console.WriteLine("Arguments were null", ConsoleColor.Red);
         console.ReadLine();
         return Task.CompletedTask;
      }

      for (int i = 0; i < Arguments.Retries; i++)
         console.WriteLine($"Trying to delete {Arguments.Name}");

      console.WriteLine($"Deleting controller with name {Arguments.Name}");
      console.ReadLine();

      return Task.CompletedTask;
   }

   public Args Arguments { get; set; }

   #endregion

   internal class Args
   {
      #region Public Properties

      [Argument("name")]
      public string Name { get; set; }

      [Argument("retries")]
      public int Retries { get; set; } = 5;

      #endregion
   }
}