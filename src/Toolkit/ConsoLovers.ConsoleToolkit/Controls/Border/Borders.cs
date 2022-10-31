// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Borders.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

public static class Borders
{
   /// <summary>Gets the default border char set.</summary>
   public static BorderCharSet Default => new()
   {
      TopLeft = '┌', 
      Top = '─', 
      Bottom = '─', 
      Left = '│',
      Right = '│', 
      TopRight = '┐',
      BottomLeft = '└',
      BottomRight = '┘'
   };

   /// <summary>Gets the char set for doubled borders.</summary>
   public static BorderCharSet Doubled => new()
   {
      TopLeft = '╔',
      Top = '═',
      Bottom = '═',
      Left = '║',
      Right = '║',
      TopRight = '╗',
      BottomLeft = '╚',
      BottomRight = '╝'
   };

   public static BorderCharSet Asterix => new()
   {
      TopLeft = '*',
      Top = '*',
      Bottom = '*',
      Left = '*',
      Right = '*',
      TopRight = '*',
      BottomLeft = '*',
      BottomRight = '*'
   };
}