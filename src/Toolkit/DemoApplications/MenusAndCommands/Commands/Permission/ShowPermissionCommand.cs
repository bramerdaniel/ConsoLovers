// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowPermissionCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;

internal class ShowPermissionCommand : IAsyncCommand<ShowPermissionCommand.ShowPermissionArgs>, IAsyncMenuCommand, IMenuArgumentInitializer
{
   private readonly IConsole console;

   internal class ShowPermissionArgs
   {
      [Argument("count")]
      public int Count { get; set; }
      
   }

   public ShowPermissionCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      console.WriteLine($"This command was called with custom initialized arguments");
      console.WriteLine($"{nameof(Arguments.Count)} = {Arguments.Count}");
      return Task.CompletedTask;
   }

   public ShowPermissionArgs Arguments { get; set; }

   public void InitializeArguments(IArgumentInitializationContext context)
   {
      console.WriteLine($"The argument count is initialized by a custom {nameof(IMenuArgumentInitializer)}", ConsoleColor.DarkYellow);

      context.InitializeArgument(nameof(ShowPermissionArgs.Count));
   }

   public async Task ExecuteAsync(IMenuExecutionContext context, CancellationToken cancellationToken)
   {
      await ExecuteAsync(cancellationToken);
      console.ReadLine();
   }
}