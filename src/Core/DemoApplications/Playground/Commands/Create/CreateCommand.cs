// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public class CreateCommand : IAsyncCommand<CreateArgs>
{
   private readonly IExecutionEngine executionEngine;

   public CreateCommand(IExecutionEngine executionEngine)
   {
      this.executionEngine = executionEngine ?? throw new ArgumentNullException(nameof(executionEngine));
   }

   public CreateArgs Arguments { get; set; } = null!;

   public async Task ExecuteAsync(CancellationToken cancellationToken)
   {
      await executionEngine.ExecuteCommandAsync(Arguments, cancellationToken);
   }
}