// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteControllerCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

internal class DeleteControllerCommand : IAsyncCommand<DeleteControllerCommand.Args>, IAsyncMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public DeleteControllerCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region IAsyncCommand<Args> Members

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

      return Task.CompletedTask;
   }

   public Args Arguments { get; set; }

   #endregion

   #region IAsyncMenuCommand Members

   public async Task ExecuteAsync(IMenuExecutionContext context, CancellationToken cancellationToken)
   {
      await ExecuteAsync(cancellationToken);
      console.ReadLine();
   }

   #endregion

   internal class Args
   {
      #region Public Properties

      [Argument("force")]
      [MenuArgument(Visibility = ArgumentVisibility.Hidden)]
      public bool Force { get; set; } = false;

      [Argument("name")]
      [MenuArgument("Name", Description = "The name of the controller to delete")]
      public string Name { get; set; }

      [Argument("retries")]
      [MenuArgument("Retries", Description = "The number of retries that should be performed")]
      public int Retries { get; set; } = 5;

      #endregion
   }
}