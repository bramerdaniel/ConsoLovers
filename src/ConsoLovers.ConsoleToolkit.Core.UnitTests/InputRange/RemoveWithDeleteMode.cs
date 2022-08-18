namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.InputRange
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.Input;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class RemoveWithDeleteMode
   {
      [TestMethod]
      public void EnsureRemoveAtTheEndDoesNothing()
      {
         var target = Setups.Setup.InputRange().WithText("four").WithMode(InsertionMode.Delete).Done();
         target.Remove();

         target.Length.Should().Be(4);
         target.Text.Should().Be("four");
      }

      [TestMethod]
      public void AfterMovingBackwardsRemoveShouldDeleteSecondFromLast()
      {
         var target = Setups.Setup.InputRange().WithText("four").WithMode(InsertionMode.Delete).Done();
         target.Move(-1);
         target.Remove();

         target.Length.Should().Be(3);
         target.Text.Should().Be("fou");
      }

      [TestMethod]
      public void AfterMovingToStartRemoveFirst()
      {
         var target = Setups.Setup.InputRange().WithText("four").WithMode(InsertionMode.Delete).Done();
         target.Move(-4);
         target.Remove();

         target.Length.Should().Be(3);
         target.Text.Should().Be("our");
      }
   }
}