// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCommamd.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class TestCommamd : ICommand<TestArgs>
{
   public void Execute()
   {
      Console.WriteLine($"Number = {Arguments.Number}");
   }

   public TestArgs Arguments { get; set; }
}