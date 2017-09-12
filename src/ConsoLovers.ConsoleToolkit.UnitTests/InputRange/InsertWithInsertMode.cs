namespace ConsoLovers.UnitTests.InputRange
{
   using System.Diagnostics.CodeAnalysis;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class InsertWithInsertMode
   {
      [TestMethod]
      public void EnsureInsertingAtTheEndWorksCorrectly()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMaximumLength(5)
            .Done();

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(5);
         target.Text.Should().Be("ooooX");

         target.Insert('X').Should().BeFalse();
         target.Position.Should().Be(5);
         target.Text.Should().Be("ooooX");
      }

      [TestMethod]
      public void EnsureInsertingAtTheStartWorksCorrectly()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMaximumLength(5)
            .Done();

         target.Move(-4);
         target.Position.Should().Be(0);

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(1);
         target.Text.Should().Be("Xoooo");

         target.Insert('X').Should().BeFalse();
         target.Position.Should().Be(1);
         target.Text.Should().Be("Xoooo");
      }


      [TestMethod]
      public void EnsureInsertingInTheMiddleWorksCorrectly()
      {
         var target = Setups.Setup.InputRange()
            .WithText("oooo")
            .WithMaximumLength(5)
            .Done();

         target.Move(-2);
         target.Position.Should().Be(2);

         target.Insert('X').Should().BeTrue();
         target.Position.Should().Be(3);
         target.Text.Should().Be("ooXoo");

         target.Insert('X').Should().BeFalse();
         target.Position.Should().Be(3);
         target.Text.Should().Be("ooXoo");
      }

   }
}