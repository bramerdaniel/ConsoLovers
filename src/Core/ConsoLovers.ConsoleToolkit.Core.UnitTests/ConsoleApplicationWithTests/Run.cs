// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Run.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils;

   using FluentAssertions;

   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Run
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureApplicationRunsWithArgumentsWhenNoDefaultCommandIsSpecified()
      {
         var applicationLogic = new SomeApplicationLogic();
         var executable = ConsoleApplication.WithArguments<ArgumentsWithoutDefaultCommands>()
            .AddSingleton(typeof(IApplicationLogic), applicationLogic)
            .Run("string=forTheApplication");

         executable.Arguments.Execute.Should().BeNull();
         executable.Arguments.DefaultCommand.Should().BeNull();
         executable.Arguments.String.Should().Be("forTheApplication");

         SomeApplicationLogic.Executed.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureApplicationRunsWithoutArgumentsWhenNoDefaultCommandIsSpecifiedAndNoArgumentsAreGiven()
      {
         var applicationLogic = new SomeApplicationLogic();
         var executable = ConsoleApplication.WithArguments<ArgumentsWithoutDefaultCommands>()
            .AddSingleton(typeof(IApplicationLogic), applicationLogic)
            .Run();

         executable.Arguments.Execute.Should().BeNull();
         executable.Arguments.DefaultCommand.Should().BeNull();
         executable.Arguments.String.Should().BeNull();
         SomeApplicationLogic.Executed.Should().BeTrue();
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
      public void EnsureSpecifiedCommandIsCalled()
      {
         var executable = ConsoleApplication.WithArguments<ArgumentsWithGenericCommand>()
            .Run("execute string=someValue int=30");

         executable.Arguments.Execute.EnsureExecuted();
         executable.Arguments.Execute.Arguments.String.Should().Be("someValue");
         executable.Arguments.Execute.Arguments.Int.Should().Be(30);
      }

      [TestMethod]
      public void EnsureArgumentImplementingIApplicationLogicAreRegisteredAutomatically()
      {
         var executable = ConsoleApplication.WithArguments<SomeApplicationLogic>()
            .UseServiceProviderFactory(new DefaultServiceProviderFactory())
            .Run();

         SomeApplicationLogic.Executed.Should().BeTrue();
         //SomeApplicationLogic.Instances.Should().Be(1);
      }

      #endregion
   }
}