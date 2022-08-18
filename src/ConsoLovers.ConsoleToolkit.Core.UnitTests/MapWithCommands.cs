// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapWithCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.IO;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

   using FluentAssertions;

   using JetBrains.Annotations;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public partial class MapWithCommands : EngineTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureEvenPrivateCommandsCanBeSet()
      {
         var arguments = GetTarget<CommandWithPrivateSetter>().Map<CommandWithPrivateSetter>(new[] { "execute", "help" });
         arguments.Execute.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureExceptionForInvalidParameterClasses()
      {
         var target = GetTarget<InvalidCommands>();
         target.Invoking(t => t.Map<InvalidCommands>(new[] { "execute" }))
            .Should().Throw<ArgumentException>();
      }

      [TestMethod]
      public void EnsureCommandPropertiesImplementICommand()
      {
         var target = GetTarget();
         target.Invoking(t => t.Map<InvalidCommands>(new[] { "noCommand" })).Should().Throw<ArgumentException>();
      }


      private T DoMap<T>(string[] args)
         where T : class, new()
      {
         return GetTarget<T>().Map<T>(args);
      }

      [TestMethod]
      public void EnsureOtherParametersAreSet()
      {
         var arguments = DoMap<ApplicationCommands>(new[] { "execute", "help" });
         arguments.Execute.Should().NotBeNull();
         arguments.Help.Should().BeTrue();

         arguments = DoMap<ApplicationCommands>(new[] { "help" });
         arguments.Execute.Should().BeNull();
         arguments.Help.Should().BeTrue();
      }

      [TestMethod]
      public void MapTheCommandAndItsArguments()
      {
         var arguments = DoMap<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-silent" });
         arguments.Execute.Should().NotBeNull();
      }

      #endregion

   }
}