// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModifyCommamd.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ModifyCommamd : IAsyncCommand<CreateArgs>
{
   #region IAsyncCommand<CreateArgs> Members

   public CreateArgs Arguments { get; set; } = null!;

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }

   #endregion
}