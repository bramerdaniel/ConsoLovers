// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleProgress.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ProgressDemo
{
   using System;

   public class ConsoleProgress
   {
      #region Constants and Fields

      private readonly double maximum;

      private int originalLeft;

      private int originalTop;

      #endregion

      #region Constructors and Destructors

      public ConsoleProgress()
         : this(100)
      {
      }

      public ConsoleProgress(double maximum)
      {
         if (maximum < 0)
            throw new ArgumentOutOfRangeException(nameof(maximum));

         this.maximum = maximum;
         originalLeft = Console.CursorLeft;
         originalTop = Console.CursorTop;

         Writer = new ConsoleProgressWriter(Console.CursorTop, Console.CursorLeft);
      }

      public IProgressWriter Writer { get; set; }

      #endregion

      #region Public Properties

      public int LeftMargin { get; set; } = 2;

      public int RightMargin { get; set; } = 2;

      #endregion

      #region Public Methods and Operators

      public void Update(double value, string text)
      {
         var percentage = value / maximum;
         var valueInPercent = percentage * 100;

         Console.CursorTop = originalTop;
         Console.CursorLeft = LeftMargin;

         Writer.DrawValue(new ProgressInfo(valueInPercent, Console.WindowWidth - LeftMargin - RightMargin, text));
      }

      #endregion

      #region Methods

      #endregion
   }
}