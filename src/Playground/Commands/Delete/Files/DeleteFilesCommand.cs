// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteFilesCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.Files;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class DeleteFilesCommand : IAsyncCommand<DeleteFileArgs>
{
   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      if (Arguments.Files == null)
         return Task.CompletedTask;

      foreach (var file in Arguments.Files)
         Console.WriteLine($"Deleting file {file}");

      return Task.CompletedTask;
   }

   public DeleteFileArgs Arguments { get; set; } = null!;
}