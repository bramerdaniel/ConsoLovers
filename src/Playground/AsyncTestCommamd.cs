﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncTestCommamd.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class AsyncTestCommamd : IAsyncCommand<CreateArgs>
{
   public CreateArgs Arguments { get; set; }

   public Task ExecuteAsync()
   {
      
      return Task.CompletedTask;
   }
}