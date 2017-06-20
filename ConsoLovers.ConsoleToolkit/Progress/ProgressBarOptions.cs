// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Progress
{
   using System;

   public class ProgressBarOptions
   {
      #region Constants and Fields

      public static readonly ProgressBarOptions Default = new ProgressBarOptions();

      #endregion

      #region Public Properties

      public ConsoleColor? BackgroundColor { get; set; }

      public bool CollapseWhenFinished { get; set; } = true;

      public bool DisplayTimeInRealTime { get; set; } = true;

      public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.Green;

      public ConsoleColor? ForeGroundColorDone { get; set; }

      public bool ProgressBarOnBottom { get; set; }

      public char ProgressCharacter { get; set; } = '\u2588';

      #endregion
   }
}