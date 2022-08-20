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

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class IndexedArguments
   {
      [TestMethod]
      public void EnsureCommandParametersWithIndexWorksCorrectly()
      {
         var path = "C:\\SomeDirectory\\SomeFile.txt";

         var application = ConsoleApplication.WithArguments<AppArgs>()
            .Run($"-e \"{path}\" -code");

         application.Arguments.Execute.Should().NotBeNull();
         application.Arguments.Execute.Arguments.FirstName.Should().Be(path);
         application.Arguments.Execute.Arguments.Code.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureCommandParametersWithNameWorksCorrectly()
      {
            var path = "C:\\SomeDirectory\\SomeFile.txt";
            var executable = ConsoleApplication.WithArguments<AppArgs>()
               .Run($"-e firstName=\"{path}\" -code");

            executable.Arguments.Execute.Should().NotBeNull();
            executable.Arguments.Execute.Arguments.FirstName.Should().Be(path);
            executable.Arguments.Execute.Arguments.Code.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureMissingIndexedParametersWorksCorrectly()
      {
         var executable = ConsoleApplication.WithArguments<AppArgs>()
            .Run("-e -code");

         executable.Arguments.Execute.Should().NotBeNull();
         executable.Arguments.Execute.Arguments.FirstName.Should().BeNull();
         executable.Arguments.Execute.Arguments.Code.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureApplicationParameterWorksCorrectly()
      {
         var executable = ConsoleApplication.WithArguments<AppArgs>()
            .Run("-e path -code");

         executable.Arguments.Execute.Should().NotBeNull();
         executable.Arguments.Execute.Arguments.FirstName.Should().Be("path");
         executable.Arguments.Execute.Arguments.LastName.Should().BeNull();
         executable.Arguments.Execute.Arguments.Code.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureApplicationParameterWorksCorrectlyEvenWhenIndexedArgumentsAreUsedAndOneIsMissing()
      {
         var application = ConsoleApplication.WithArguments<AppArgs>()
            .Run("-e rudolf -wait");

         application.Arguments.Execute.Should().NotBeNull();
         application.Arguments.Execute.Arguments.FirstName.Should().Be("rudolf");
         application.Arguments.Execute.Arguments.LastName.Should().BeNull();
         application.Arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureTwoNamedParametersWorkInAnyOrder()
      {
         var executable = ConsoleApplication.WithArguments<AppArgs>()
            .Run("-e lastname=engelbert firstname=peter -code");

         executable.Arguments.Execute.Should().NotBeNull();
         executable.Arguments.Execute.Arguments.FirstName.Should().Be("peter");
         executable.Arguments.Execute.Arguments.LastName.Should().Be("engelbert");
         executable.Arguments.Execute.Arguments.Code.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSingleNamedParametersWorkOnWrongIndex()
      {
         var executable = ConsoleApplication.WithArguments<AppArgs>()
            .Run("-e lastName=huber -wait");

         executable.Arguments.Execute.Should().NotBeNull();
         executable.Arguments.Execute.Arguments.FirstName.Should().BeNull();
         executable.Arguments.Execute.Arguments.LastName.Should().Be("huber");
         executable.Arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureTwoIndexedParametersWorkInCorrectOrder()
      {
         var executable = ConsoleApplication.WithArguments<AppArgs>()
            .Run("-e hans müller -wait");

         executable.Arguments.Execute.Should().NotBeNull();
         executable.Arguments.Execute.Arguments.FirstName.Should().Be("hans");
         executable.Arguments.Execute.Arguments.LastName.Should().Be("müller");
         executable.Arguments.Wait.Should().BeTrue();
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
         private string lastName;

         internal static PropertyInfo FirstNameProperty => typeof(CommandArgs).GetProperty(nameof(FirstName));

         internal static PropertyInfo LastNameProperty => typeof(CommandArgs).GetProperty(nameof(LastName));

         internal static PropertyInfo CodeProperty => typeof(CommandArgs).GetProperty(nameof(Code));

         [Argument("FirstName", Index = 0)]
         public string FirstName { get; set; }

         [Argument("LastName", Index = 1)]
         public string LastName
         {
            get => lastName;
            set => lastName = value;
         }

         [Option("Code", "c")]
         public bool Code { get; set; }
      }
   }



}