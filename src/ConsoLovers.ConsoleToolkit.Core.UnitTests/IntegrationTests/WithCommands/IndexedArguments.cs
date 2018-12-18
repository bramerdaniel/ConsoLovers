// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexedArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests.WithCommands
{
   using System.Diagnostics.CodeAnalysis;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class IndexedArguments
   {
      [TestMethod]
      public void EnsureCommandParametersWithIndexWorksCorrectly()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            var path = "C:\\SomeDirectory\\SomeFile.txt";
            testContext.RunApplication($"-e \"{path}\" -code");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);

            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, path), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.CodeProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureCommandParametersWithNameWorksCorrectly()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            var path = "C:\\SomeDirectory\\SomeFile.txt";
            testContext.RunApplication($"-e firstName=\"{path}\" -code");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);

            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, path), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.CodeProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureMissingIndexedParametersWorksCorrectly()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e -code");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);

            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, "code"), Times.Never);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.CodeProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureApplicationParameterWorksCorrectly()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e path -code");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);

            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, "path"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.LastNameProperty), Times.Never);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.CodeProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureApplicationParameterWorksCorrectlyEvenWhenIndexedArgumentsAreUsedAndOneIsMissing()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e rudolf -wait");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);
           
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, "rudolf"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.LastNameProperty), Times.Never);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.WaitProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureTwoNamedParametersWorkInAnyOrder()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e lastname=engelbert firstname=peter -code");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);
            
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, "peter"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.LastNameProperty, "engelbert"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.CodeProperty, true), Times.Once);
         }

         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e firstname=peter -code lastname=engelbert");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);
            
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, "peter"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.LastNameProperty, "engelbert"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.CodeProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureSingleNamedParametersWorkOnWrongIndex()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e lastName=huber -wait");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);
            
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty), Times.Never);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.LastNameProperty, "huber"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.WaitProperty, true), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureTwoIndexedParametersWorkInCorrectOrder()
      {
         using (var testContext = new ApplicationTestContext<AppArgs>())
         {
            testContext.RunApplication("-e hans müller -wait");
            testContext.VerifyCommandExecuted<GenericCommand<CommandArgs>>();
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.ExecuteProperty), Times.Once);

            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.FirstNameProperty, "hans"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(CommandArgs.LastNameProperty, "müller"), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter(AppArgs.WaitProperty, true), Times.Once);
         }
      }
   }

   public class AppArgs
   {
      [Command("e")]
      public GenericCommand<CommandArgs> Execute { get; set; }

      [Option("Wait", "w")]
      public bool Wait { get; set; }

      internal static PropertyInfo WaitProperty => typeof(AppArgs).GetProperty(nameof(Wait));

      internal static PropertyInfo ExecuteProperty => typeof(AppArgs).GetProperty(nameof(Execute));
   }

   public class CommandArgs
   {
      internal static PropertyInfo FirstNameProperty => typeof(CommandArgs).GetProperty(nameof(FirstName));

      internal static PropertyInfo LastNameProperty => typeof(CommandArgs).GetProperty(nameof(LastName));

      internal static PropertyInfo CodeProperty => typeof(CommandArgs).GetProperty(nameof(Code));

      [Argument("FirstName", Index = 0)]
      public string FirstName { get; set; }

      [Argument("LastName", Index = 1)]
      public string LastName { get; set; }

      [Option("Code", "c")]
      public bool Code { get; set; }
   }
}