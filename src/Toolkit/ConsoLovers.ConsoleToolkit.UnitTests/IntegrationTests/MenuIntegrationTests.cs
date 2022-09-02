// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.IntegrationTests;

using System;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MenuIntegrationTests
{
   [TestMethod]
   public void EnsureCommandMustImplementArgumentInitializerForCustomInitialization()
   {
      var test = Setup.MenuCommandTest<Root>()
         .WithArgumentInitialization(ArgumentInitializationModes.Custom)
         .Done();

      test.Invoking(t => t.Execute("Run")).Should().Throw<InvalidOperationException>();
   }

   [TestMethod]
   public void EnsureArgumentInitializerIsInvoked()
   {
      var command = Setup.MenuCommandTest<Root>()
         .WithArgumentInitialization(ArgumentInitializationModes.Custom)
         .ExecuteCommand<InitializerCommand<MyArgs>>("Custom");

      command.Initialized.Should().BeTrue();
      command.WasExecutedFromMenu.Should().BeTrue();
      command.WasExecuted.Should().BeTrue();
   }

   [TestMethod]
   public void EnsureCommandArgumentsAreInitializedDuringExecution()
   {
      var command = Setup.MenuCommandTest<Root>()
         .WithArgumentInitialization(ArgumentInitializationModes.WhileExecution)
         .ConfigureInput("Count", 11)
         .ExecuteCommand<Command<MyArgs>>("Run");

      command.WasExecuted.Should().BeTrue();
      command.Arguments.Count.Should().Be(11);
   }

   public class Root
   {
      [MenuCommand("Run")]
      public Command<MyArgs> Run { get; set; }     
      
      [MenuCommand("Custom")]
      public InitializerCommand<MyArgs> Custom { get; set; }
   }

   public class MyArgs
   {
      [Argument("count")]
      [MenuArgument()]
      public int Count { get; set; }
   }

   public class DerivedArgs : SharedArgs
   {
      [MenuArgument("Count")]
      public int Count{ get; set; }
   }

   public class SharedArgs
   {
      [MenuArgument("UserName")]
      public string UserName{ get; set; }
   }
}

