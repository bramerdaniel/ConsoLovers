// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMapperSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class CommandMapperSetup<T> : SetupBase<CommandMapper<T>>
   where T : class
{
   protected override CommandMapper<T> CreateInstance()
   {

      return new CommandMapper<T>(Setup.EngineFactory().Done());
   }
}