// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputRangeSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit;

   internal class InputRangeSetup
   {
      #region Properties

      internal int Length { get; set; }

      internal InsertionMode Mode { get; set; }

      internal string Text { get; set; }

      #endregion

      #region Public Methods and Operators

      public InputRange Done()
      {
         return new InputRange(Text, Length) { Mode = Mode };
      }

      public InputRangeSetup WithMaximumLength(int length)
      {
         Length = length;
         return this;
      }

      public InputRangeSetup WithMode(InsertionMode mode)
      {
         Mode = mode;
         return this;
      }

      public InputRangeSetup WithText(string text)
      {
         Text = text;
         return this;
      }

      #endregion
   }
}