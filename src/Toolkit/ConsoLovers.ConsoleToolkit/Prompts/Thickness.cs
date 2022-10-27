// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Thickness.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

public struct Thickness
{
   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Thickness"/> structure that has the specified uniform length on each side.</summary>
   /// <param name="uniformLength">The uniform length applied to all four sides of the bounding rectangle.</param>
   public Thickness(int uniformLength) => Left = Top = Right = Bottom = uniformLength;

   /// <summary>
   ///    Initializes a new instance of the <see cref="T:System.Windows.Thickness"/> structure that has specific lengths (supplied as a
   ///    <see cref="T:System.int"/>) applied to each side of the rectangle.
   /// </summary>
   /// <param name="left">The thickness for the left side of the rectangle.</param>
   /// <param name="top">The thickness for the upper side of the rectangle.</param>
   /// <param name="right">The thickness for the right side of the rectangle.</param>
   /// <param name="bottom">The thickness for the lower side of the rectangle.</param>
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