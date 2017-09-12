// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapWithCommands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.IO;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.ArgumentEngine;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class MapWithCommands : EngineTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureEvenPrivateCommandsCanBeSet()
      {
         var arguments = GetTarget().Map<ImutableCommands>(new[] { "execute", "help" });
         arguments.Execute.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureExceptionForInvalidParameterClasses()
      {
         var target = GetTarget();
         target.Invoking(t => t.Map<InvalidCommands>(new[] { "execute" })).ShouldThrow<ArgumentException>();
      }

      [TestMethod]
      public void EnsureCommandPropertiesImplementICommand()
      {
         var target = GetTarget();
         target.Invoking(t => t.Map<InvalidCommands>(new[] { "noCommand" })).ShouldThrow<ArgumentException>();
      }


      [TestMethod]
      public void EnsureOtherParametesAreSet()
      {
         var arguments = GetTarget().Map<ApplicationCommands>(new[] { "execute", "help" });
         arguments.Execute.Should().NotBeNull();
         arguments.Help.Should().BeTrue();

         arguments = GetTarget().Map<ApplicationCommands>(new[] { "help" });
         arguments.Execute.Should().BeNull();
         arguments.Help.Should().BeTrue();
      }

      [TestMethod]
      public void MapTheCommandAndItsArguments()
      {
         var arguments = GetTarget().Map<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-silent" });
         arguments.Execute.Should().NotBeNull();
      }

      #endregion

      internal class ApplicationCommands
      {
         #region Public Properties

         [Command("Execute")]
         public Command Execute { get; set; }

         [Option("Help")]
         public bool Help { get; set; }

         #endregion
      }

      internal class ApplicationCommandsWithDefault
      {
         #region Public Properties

         [Command("Execute", IsDefaultCommand = true)]
         public Command Execute { get; set; }

         [Command("ExecuteMany")]
         public Command ExecuteMany { get; set; }

         #endregion
      }

      internal class ImutableCommands
      {
         #region Public Properties

         [Command("Execute", "e")]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute { get; private set; }

         #endregion
      }

      internal class InvalidCommands
      {
         #region Public Properties

         [Command("Execute", "e")]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute => null;

         [Command("NoCommand", "nc")]
         public FileInfo NoCommand { get; set; }

         #endregion
      }
   }
}