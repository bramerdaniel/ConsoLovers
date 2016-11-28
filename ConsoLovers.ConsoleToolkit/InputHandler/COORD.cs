// --------------------------------------------------------------------------------------------------------------------
// <copyright file="COORD.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   public struct COORD
   {
      #region Constants and Fields

      public short X;

      public short Y;

      #endregion

      #region Constructors and Destructors

      public COORD(short x, short y)
      {
         X = x;
         Y = y;
      }

      #endregion
   }
}