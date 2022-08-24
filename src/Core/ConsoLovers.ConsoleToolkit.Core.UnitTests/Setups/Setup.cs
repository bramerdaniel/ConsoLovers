// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

   using FluentSetups;

   [FluentRoot]
   internal partial class Setup
   {
      #region Public Methods and Operators

      public static ApplicationBuilderSetup<T> ApplicationBuilder<T>()
         where T : class
      {
         return new ApplicationBuilderSetup<T>();
      }

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

      public static CommandMapperSetup<T> CommandMapper<T>()
         where T : class
      {
         return new CommandMapperSetup<T>();
      }

      public static HelpCommandSetup HelpCommand()
      {
         return new HelpCommandSetup();
      }

      public static InputRangeSetup InputRange()
      {
         return new InputRangeSetup();
      }

      public static MockSetup MockFor()
      {
         return new MockSetup();
      }

      public static TypeHelpProviderSetup TypeHelpProvider()
      {
         return new TypeHelpProviderSetup();
      }

      #endregion

      public static ExecutionPipelineSetup ExecutionPipeline()
      {
         return new ExecutionPipelineSetup();
      }
   }
}