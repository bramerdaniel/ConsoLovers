// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncTestCommamd.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class AsyncTestCommamd : IAsyncCommand<TestArgs>
{
   public TestArgs Arguments { get; set; }

   public Task ExecuteAsync()
   {
      Console.WriteLine($"Number = {Arguments.Number}");
      return Task.CompletedTask;
   }
}