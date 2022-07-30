// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public class CreateCommand : IAsyncCommand<CreateArgs>
{
   private readonly ICommandExecutor commandExecutor;

   public CreateCommand(ICommandExecutor commandExecutor)
   {
      this.commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
   }

   public CreateArgs Arguments { get; set; } = null!;

   public async Task ExecuteAsync(CancellationToken cancellationToken)
   {
      await commandExecutor.ExecuteCommandAsync(Arguments, cancellationToken);
   }
}