namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.InputRange
{
   using System.Diagnostics.CodeAnalysis;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class MoveTests
   {
      [TestMethod]
      public void EnsureMovingBackwardWorks()
      {
         var target = Setups.Setup.InputRange().WithText("four").Done();
         target.Position.Should().Be(4);

         target.Move(-1).Should().BeTrue();
         target.Position.Should().Be(3);

         target.Move(-1).Should().BeTrue();
         target.Position.Should().Be(2);

         target.Move(-1).Should().BeTrue();
         target.Position.Should().Be(1);

         target.Move(-1).Should().BeTrue();
         target.Position.Should().Be(0);

         target.Move(-1).Should().BeFalse();
         target.Position.Should().Be(0);
      }

      [TestMethod]
      public void EnsureMovingForwardWorks()
      {
         var target = Setups.Setup.InputRange().WithText("four").Done();
         target.Position.Should().Be(4);

         target.Move(-4).Should().BeTrue();
         target.Position.Should().Be(0);

         target.Move(1).Should().BeTrue();
         target.Position.Should().Be(1);

         target.Move(1).Should().BeTrue();
         target.Position.Should().Be(2);

         target.Move(1).Should().BeTrue();
         target.Position.Should().Be(3);

         target.Move(1).Should().BeTrue();
         target.Position.Should().Be(4);

         target.Move(1).Should().BeFalse();
      }
   }
}