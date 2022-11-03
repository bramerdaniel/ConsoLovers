// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Thickness.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

public struct Thickness
{
   #region Constructors and Destructors

   public Thickness(int uniformLength) => Left = Top = Right = Bottom = uniformLength;

   public Thickness(int leftRight, int topBottom)
   {
      Left = Right = leftRight;
      Top = Bottom = topBottom;
   }

   public Thickness(int left, int top, int right, int bottom)
   {
      Left = left;
      Top = top;
      Right = right;
      Bottom = bottom;
   }

   #endregion

   #region Public Properties

   public int Bottom { get; set; }

   public int Left { get; set; }

   public int Right { get; set; }

   public int Top { get; set; }

   #endregion
}