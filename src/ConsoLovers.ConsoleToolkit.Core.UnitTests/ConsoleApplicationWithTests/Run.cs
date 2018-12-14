// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Run.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests
{
   using System;
   using System.Diagnostics.CodeAnalysis;

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
         using (var testContext = new ApplicationTestContext<TestApplication<ArgumentsWithGenericCommand>>())
         {
            testContext.RunApplication("execute string=someValue int=30");

            testContext.Application.Verify(a => a.Run(), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(), Times.Once);

            testContext.Application.Verify(a => a.RunWith(), Times.Never);
            testContext.Application.Verify(a => a.RunWithoutArguments(), Times.Never);

            testContext.Commands.Verify(x => x.Execute("GenericExecute"), Times.Once);
            testContext.Commands.Verify(x => x.Argument("string", "someValue"), Times.Once);
            testContext.Commands.Verify(x => x.Argument("int", 30), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureDefaultCommandIsCalledWhenThereAreArgumentsButNoCommand()
      {
         using (var testContext = new ApplicationTestContext<TestApplication<ArgumentsWithGenericDefaultCommand>>())
         {
            testContext.RunApplication("string=someValue");

            testContext.Application.Verify(a => a.Run(), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(), Times.Once);

            testContext.Application.Verify(a => a.RunWith(), Times.Never);
            testContext.Application.Verify(a => a.RunWithoutArguments(), Times.Never);

            testContext.Commands.Verify(x => x.Execute("GenericExecute"), Times.Once);
            testContext.Commands.Verify(x => x.Argument("string","someValue"), Times.Once);
            testContext.Commands.Verify(x => x.Argument("int", It.IsAny<object>()), Times.Never);
         }
      }

      [TestMethod]
      public void EnsureExceptionIsThrownWhenTwoDefaultCommandAreSpecified()
      {
         using (var testContext = new ApplicationTestContext<TestApplication<ArgumentsTwoDefaultCommands>>())
         {
            testContext.Invoking(x => x.RunApplication()).ShouldThrow<InvalidOperationException>();
         }
      }

      [TestMethod]
      public void EnsureApplicationRunsWithArgumentsWhenNoDefaultCommandIsSpecified()
      {
         using (var testContext = new ApplicationTestContext<TestApplication<ArgumentsWithoutDefaultCommands>>())
         {
            testContext.RunApplication("string=forTheApplication");

            testContext.Application.Verify(a => a.Run(), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(), Times.Never);

            testContext.Application.Verify(a => a.RunWith(), Times.Once);
            testContext.Application.Verify(a => a.RunWithoutArguments(), Times.Never);
            testContext.Application.Verify(a => a.Argument("string", "forTheApplication"), Times.Once);

            testContext.Commands.Verify(x => x.Execute("DefaultExecute"), Times.Never);
            testContext.Commands.Verify(x => x.Execute("GenericExecute"), Times.Never);
         }
      }

      [TestMethod]
      public void EnsureApplicationRunsWithoutArgumentsWhenNoDefaultCommandIsSpecifiedAndNoArgumentsAreGiven()
      {
         using (var testContext = new ApplicationTestContext<TestApplication<ArgumentsWithoutDefaultCommands>>())
         {
            testContext.RunApplication(string.Empty);

            testContext.Application.Verify(a => a.Run(), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(), Times.Never);

            testContext.Application.Verify(a => a.RunWith(), Times.Once);
            testContext.Application.Verify(a => a.RunWithoutArguments(), Times.Once);
            testContext.Application.Verify(a => a.Argument("string", null), Times.Once);

            testContext.Commands.Verify(x => x.Execute("DefaultExecute"), Times.Never);
            testContext.Commands.Verify(x => x.Execute("GenericExecute"), Times.Never);
         }
      }
   }
}