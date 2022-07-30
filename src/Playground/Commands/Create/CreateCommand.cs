// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class CreateCommand : IAsyncCommand<CreateArgs>
{
   public CreateArgs Arguments { get; set; } = null!;

   public async Task ExecuteAsync(CancellationToken cancellationToken)
   {
      await CommandExecutor.ExecuteCommandAsync<CreateArgs>(Arguments, cancellationToken);
   }
}