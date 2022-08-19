// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Run.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Run
   {
      [TestMethod]
      public void EnsureSpecifiedCommandIsCalled()
      {
         var executable = ConsoleApplication.WithArguments<ArgumentsWithGenericCommand>()
            .Run("execute string=someValue int=30");

         executable.Arguments.Execute.EnsureExecuted();
         executable.Arguments.Execute.Arguments.String.Should().Be("someValue");
         executable.Arguments.Execute.Arguments.Int.Should().Be(30);
      }

      [TestMethod]
      public void EnsureDefaultCommandIsCalledWhenThereAreArgumentsButNoCommand()
      {
         var executable = ConsoleApplication.WithArguments<ArgumentsWithGenericDefaultCommand>()
            .Run("string=someValue");

         executable.Arguments.Execute.EnsureExecuted();
         executable.Arguments.Execute.Arguments.String.Should().Be("someValue");
      }

      [TestMethod]
      public void EnsureExceptionIsThrownWhenTwoDefaultCommandAreSpecified()
      {
         var executable = ConsoleApplication.WithArguments<ArgumentsTwoDefaultCommands>();
         executable.Invoking(x => x.Run()).Should().Throw<InvalidOperationException>();
      }

      [TestMethod]
      public void EnsureApplicationRunsWithArgumentsWhenNoDefaultCommandIsSpecified()
      {
         var executable = ConsoleApplication.WithArguments<ArgumentsWithoutDefaultCommands>()
            .AddSingleton(typeof(IApplicationLogic), typeof(ArgumentsWithoutDefaultCommands))
            .Run("string=forTheApplication");

         executable.Arguments.Execute.Should().BeNull();
         executable.Arguments.DefaultCommand.Should().BeNull();
         executable.Arguments.String.Should().Be("forTheApplication");

         ArgumentsWithoutDefaultCommands.Executed.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureApplicationRunsWithoutArgumentsWhenNoDefaultCommandIsSpecifiedAndNoArgumentsAreGiven()
      {
         using (var testContext = new ApplicationTestContext<ArgumentsWithoutDefaultCommands>())
         {
            testContext.RunApplication(string.Empty);

            testContext.Application.Verify(a => a.RunWithCommand(It.IsAny<ICommand>()), Times.Never);

            testContext.Application.Verify(a => a.RunWithAsync(It.IsAny<ArgumentsWithoutDefaultCommands>()), Times.Once);
            testContext.Application.Verify(a => a.RunWithoutArguments(), Times.Once);
            testContext.Application.Verify(a => a.Argument("string", null), Times.Once);

            testContext.Commands.Verify(x => x.Execute("DefaultExecute"), Times.Never);
            testContext.Commands.Verify(x => x.Execute("GenericExecute"), Times.Never);
         }
      }
   }
}