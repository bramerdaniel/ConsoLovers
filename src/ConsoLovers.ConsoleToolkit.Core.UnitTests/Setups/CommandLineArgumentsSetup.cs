// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentsSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System.Collections.Concurrent;
using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class CommandLineArgumentsSetup : SetupBase<IDictionary<string, CommandLineArgument>>
{
   private IDictionary<string, CommandLineArgument> arguments = new Dictionary<string, CommandLineArgument>();

   public CommandLineArgumentsSetup Add(string key, CommandLineArgument commandLineArgument)
   {
      arguments.Add(key, commandLineArgument);
      return this;
   }
   
   protected override IDictionary<string, CommandLineArgument> CreateInstance()
   {
      return arguments;
   }
}