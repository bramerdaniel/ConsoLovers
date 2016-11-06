// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserTestBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class ParserTestBase
   {
      #region Methods

      protected CommandLineArgumentParser GetTarget()
      {
         return Setup.CommandLineArgumentParser().Done();
      }

      protected IDictionary<string, string> Parse(params string[] parameters)
      {
         return GetTarget().Parse(parameters);
      }

      protected IDictionary<string, string> Parse(string[] parameters, bool caseSensitive)
      {
         return GetTarget().Parse(parameters, caseSensitive);
      }

      #endregion
   }
}