// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CovertValueTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class CovertValueTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureBooleanValuesAreMappedCorrectly()
      {
         Convert<bool>("true").Should().BeTrue();
         Convert<bool>("false").Should().BeFalse();
         Convert<bool>("True").Should().BeTrue();
         Convert<bool>("False").Should().BeFalse();
         Convert<bool>("TRUE").Should().BeTrue();
         Convert<bool>("FALSE").Should().BeFalse();
         Convert<bool>("").Should().BeTrue();
         Convert<bool>(null).Should().BeTrue();
      }

      [TestMethod]
      public void EnsureCaseInsensiveEnumValuesAreConvertedCorrectly()
      {
         Convert<TestCommandType>("Init").Should().Be(TestCommandType.Init);
         Convert<TestCommandType>("Backup").Should().Be(TestCommandType.Backup);
         Convert<TestCommandType>("Restore").Should().Be(TestCommandType.Restore);

         Convert<TestCommandType>("init").Should().Be(TestCommandType.Init);
         Convert<TestCommandType>("BACKUP").Should().Be(TestCommandType.Backup);
         Convert<TestCommandType>("RestorE").Should().Be(TestCommandType.Restore);
      }

      [TestMethod]
      public void EnsureDoubleValuesAreMappedCorrectly()
      {
         Convert<double>("0.0").Should().Be(0.0);
         Convert<double>("1.0").Should().Be(1.0);
         Convert<double>("25.1").Should().Be(25.1);
         Convert<double>("4.2").Should().Be(4.2);
         Convert<double>("-1.0").Should().Be(-1.0);
         Convert<double>("-234.123").Should().Be(-234.123);
      }

      [TestMethod]
      public void EnsureIntegerValuesAreMappedCorrectly()
      {
         Convert<int>("0").Should().Be(0);
         Convert<int>("1").Should().Be(1);
         Convert<int>("25").Should().Be(25);
         Convert<int>("-10").Should().Be(-10);
      }

      [TestMethod]
      public void EnsureInvalidEnumValueThrowsException()
      {
         this.Invoking(x => Convert<TestCommandType>("INVALID")).ShouldThrow<CommandLineArgumentException>().WithMessage("INVALID");
      }

      [TestMethod]
      public void EnsureInvalidValueThrowsException()
      {
         this.Invoking(x => Convert<bool>("TURE")).ShouldThrow<CommandLineArgumentException>().WithMessage("TURE");
         this.Invoking(x => Convert<int>("5.0")).ShouldThrow<CommandLineArgumentException>().WithMessage("5.0");
         this.Invoking(x => Convert<double>("5.0.0")).ShouldThrow<CommandLineArgumentException>().WithMessage("5.0.0");
      }

      [TestMethod]
      public void EnsureStringValuesAreMappedCorrectly()
      {
         Convert<string>("true").Should().Be("true");
         Convert<string>("false").Should().Be("false");
      }

      #endregion

      #region Methods

      [DebuggerStepThrough]
      private static T Convert<T>(string value)
      {
         return (T)MapperBase.ConvertValue(typeof(T), value, (t, v) => v);
      }

      #endregion
   }

   class Args
   {
      #region Public Properties

      [Command("Init", "i")]
      public TestCommandType Command { get; set; }

      #endregion
   }
}