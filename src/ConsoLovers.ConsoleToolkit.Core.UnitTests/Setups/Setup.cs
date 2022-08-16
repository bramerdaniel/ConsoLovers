// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

   public class Setup
   {
      #region Public Methods and Operators

      public static ArgumentClassInfoSetup ArgumentClassInfo()
      {
         return new ArgumentClassInfoSetup();
      }

      public static ArgumentMapperSetup ArgumentMapper()
      {
         return new ArgumentMapperSetup();
      }

      public static CommandLinerParserSetup CommandLineArgumentParser()
      {
         return new CommandLinerParserSetup();
      }

      public static CommandLineArgumentsSetup CommandLineArguments()
      {
         return new CommandLineArgumentsSetup();
      }

      public static CommandLineEngineSetup CommandLineEngine()
      {
         return new CommandLineEngineSetup();
      }

      public static EngineFactorySetup EngineFactory()
      {
         return new EngineFactorySetup();
      }

      public static HelpCommandSetup HelpCommand()
      {
         return new HelpCommandSetup();
      }

      public static MockSetup MockFor()
      {
         return new MockSetup();
      }

      #endregion

      public static CommandMapperSetup<T> CommandMapper<T>()
         where T : class
      {
         return new CommandMapperSetup<T>();
      }
   }
}