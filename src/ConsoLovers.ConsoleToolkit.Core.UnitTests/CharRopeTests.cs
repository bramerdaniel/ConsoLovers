// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharRopeTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class CharRopeTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureBoundsDoNotCauseExceptions()
      {
         var target = CreateTarget("abc");
         var charInfo = target.First();

         charInfo.Current.Should().Be('a');
         charInfo.Previous.Should().Be(char.MinValue);
         charInfo.Next.Should().Be('b');
         charInfo.InsideQuotes().Should().BeFalse();

         charInfo = target.Last();

         charInfo.Current.Should().Be('c');
         charInfo.Previous.Should().Be('b');
         charInfo.Next.Should().Be(char.MinValue);
         charInfo.InsideQuotes().Should().BeFalse();
      }

      [TestMethod]
      public void EnsureNormalStringWorksCorrectly()
      {
         var target = CreateTarget("abc");
         var charInfo = target.ElementAt(1);

         charInfo.Current.Should().Be('b');
         charInfo.Previous.Should().Be('a');
         charInfo.Next.Should().Be('c');
         charInfo.InsideQuotes().Should().BeFalse();
      }

      #endregion

      #region Methods

      private static CharRope CreateTarget(string original)
      {
         return new CharRope(original);
      }

      #endregion
   }
}