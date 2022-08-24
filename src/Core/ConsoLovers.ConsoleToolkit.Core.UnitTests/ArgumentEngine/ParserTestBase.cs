// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserTestBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System.Collections.Generic;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   public class ParserTestBase
   {
      #region Methods

      protected CommandLineArgumentParser GetTarget()
      {
         return Setup.CommandLineArgumentParser().Done();
      }

      protected ICommandLineArguments Parse(params string[] parameters)
      {
         return GetTarget().ParseArguments(parameters);
      }

      #endregion
   }
}