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

      public static ArgumentEngineSetup ArgumentEngine()
      {
         return new ArgumentEngineSetup();
      }

      #endregion

      public static CommandLinerParserSetup CommandLineArgumentParser()
      {
         return new CommandLinerParserSetup();
      }
   }

   public class CommandLinerParserSetup
   {
      public CommandLineArgumentParser Done()
      {
         return new CommandLineArgumentParser();
      }
   }
   public class ArgumentEngineSetup
   {
      public CommandLineEngine Done()
      {
         return new CommandLineEngine();
      }
   }
}