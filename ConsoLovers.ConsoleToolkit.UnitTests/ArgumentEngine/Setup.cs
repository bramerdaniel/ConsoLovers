// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   public class Setup
   {
      #region Public Methods and Operators

      public static ArgumentEngineSetup ArgumentEngine()
      {
         return new ArgumentEngineSetup();
      }

      public static CommandLinerParserSetup CommandLineArgumentParser()
      {
         return new CommandLinerParserSetup();
      }

      public static EngineFactorySetup EngineFactory()
      {
         return new EngineFactorySetup();
      }

      #endregion

      public static ArgumentMapperSetup ArgumentMapper()
      {
         return new ArgumentMapperSetup();
      }
   }
}