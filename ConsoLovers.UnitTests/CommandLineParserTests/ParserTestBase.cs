// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserTestBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.CommandLineParserTests
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class ParserTestBase
   {
      #region Methods

      protected ArgumentEngine GetTarget()
      {
         return Setup.CommandLineParser().Done();
      }

      protected IDictionary<string, string> Parse(string[] parameters)
      {
         return GetTarget().Parse(parameters);
      }

      #endregion
   }
}