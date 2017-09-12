namespace ConsoLovers.UnitTests.InputRange
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class InsertWithDeleteMode
   {
      [TestMethod]
      public void EnsureInsertingAtTheEndWorkCorrectly()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMode(InsertionMode.Delete)
            .Done();

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(5);
         target.Text.Should().Be("ooooX");
      }

      [TestMethod]
      public void EnsureInsertingIntTheMiddleWorkCorrectly()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMode(InsertionMode.Delete)
            .Done();

         target.Move(-2);
         target.Position.Should().Be(2);

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(3);
         target.Text.Should().Be("ooXo");
      }

      [TestMethod]
      public void EnsureInsertingAtTheStartWorksCorrectly()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMode(InsertionMode.Delete)
            .Done();

         target.Move(-4);
         target.Position.Should().Be(0);

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(1);
         target.Text.Should().Be("Xooo");
      }

      [TestMethod]
      public void EnsureInsertingWihMaximumLength()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMode(InsertionMode.Delete)
            .WithMaximumLength(4)
            .Done();

         target.Move(-1);
         target.Position.Should().Be(3);

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(4);
         target.Text.Should().Be("oooX");

         target.Insert('X').Should().BeFalse();
         target.Position.Should().Be(4);
         target.Text.Should().Be("oooX");
      }
   }
}