// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class Setup
   {
      #region Public Methods and Operators

      public static CommandLineParserSetup CommandLineParser()
      {
         return new CommandLineParserSetup();
      }

      #endregion
   }

   public class CommandLineParserSetup
   {
      public ArgumentEngine Done()
      {
         return new ArgumentEngine();
      }
   }
}