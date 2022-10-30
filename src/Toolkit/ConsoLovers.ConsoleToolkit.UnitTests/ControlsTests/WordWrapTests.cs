// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordWrapTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests
{
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class WordWrapTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void WordWrap_BreaksWordExceedingLimitOfSeveralLines()
      {
         var rows = "foobar_line_foobar".Wrap(6);
         Assert.AreEqual(3, rows.Count);
         Assert.AreEqual("foobar", rows[0]);
         Assert.AreEqual("_line_", rows[1]);
         Assert.AreEqual("foobar", rows[2]);
      }

      [TestMethod]
      public void EnsureWordWrapBreaksWordExceedingLineLimit()
      {
         var rows = "foobarhello".Wrap(7);
         Assert.AreEqual(2, rows.Count);
         Assert.AreEqual("foobarh", rows[0]);
         Assert.AreEqual("ello", rows[1]);
      }

      [TestMethod]
      public void EnsureBreaksWordsExceedingLineLimitAndBreaksBetweenWordThatFitsWithinLimit()
      {
         var lines = "lorem ipsum testing7890xyz 12345 abc".Wrap(12);
         Assert.AreEqual(4, lines.Count);
         Assert.AreEqual("lorem ipsum", lines[0]);
         Assert.AreEqual("testing7890x", lines[1]);
         Assert.AreEqual("yz 12345", lines[2]);
         Assert.AreEqual("abc", lines[3]);
      }

      [TestMethod]
      public void WordWrap_SplitsTextIntoAllowableLengthsAndAvoidsBreakingWords()
      {
         var lines = "12345 abcd 1234".Wrap(7);
         Assert.AreEqual(3, lines.Count);
         Assert.AreEqual("12345", lines[0]);
         Assert.AreEqual("abcd", lines[1]);
         Assert.AreEqual("1234", lines[2]);
      }

      #endregion
   }
}