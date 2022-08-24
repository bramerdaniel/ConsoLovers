// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentsSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System.Collections.Concurrent;
using System.Collections.Generic;

public class CommandLineArgumentsSetup : SetupBase<CommandLineArgumentList>
{
   private readonly CommandLineArgumentList arguments = new();

   public CommandLineArgumentsSetup Add(CommandLineArgument commandLineArgument)
   {
      arguments.Add(commandLineArgument);
      return this;
   }
   
   protected override CommandLineArgumentList CreateInstance()
   {
      return arguments;
   }
}