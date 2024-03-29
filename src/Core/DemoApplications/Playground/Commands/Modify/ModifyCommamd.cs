﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModifyCommamd.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;

public class ModifyCommamd : IAsyncCommand<ModifyArgs>
{
   #region IAsyncCommand<CreateArgs> Members

   public ModifyArgs Arguments { get; set; } = null!;

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }

   #endregion
}