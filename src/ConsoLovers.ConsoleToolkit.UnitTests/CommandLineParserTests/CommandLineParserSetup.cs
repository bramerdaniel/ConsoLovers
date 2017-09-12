// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineParserSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.CommandLineParserTests
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class CommandLineParserSetup
   {
      #region Public Methods and Operators

      public ArgumentEngine Done()
      {
         return new ArgumentEngine();
      }

      #endregion
   }
}