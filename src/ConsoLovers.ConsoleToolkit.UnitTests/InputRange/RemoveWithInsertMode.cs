// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.UnitTests.InputRange
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class RemoveWithInsertMode
   {
      [TestMethod]
      public void EnsureRemoveDeletesTheLastByDefault()
      {
         var target = Setup.InputRange().WithText("four").Done();
         target.Remove();

         target.Length.Should().Be(3);
         target.Text.Should().Be("fou");
      }

      [TestMethod]
      public void AfterMovingBackwardsRemoveShouldDeleteSecondFromLast()
      {
         var target = Setup.InputRange().WithText("four").Done();
         target.Move(-1);
         target.Remove();

         target.Length.Should().Be(3);
         target.Text.Should().Be("for");
      }

      [TestMethod]
      public void AfterMovingToStartRemoveShouldDoNothing()
      {
         var target = Setup.InputRange().WithText("four").Done();
         target.Move(-4);
         target.Remove();

         target.Length.Should().Be(4);
         target.Text.Should().Be("four");
      }
   }
}