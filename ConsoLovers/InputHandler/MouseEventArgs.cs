// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;

   public class MouseEventArgs : EventArgs
   {
      #region Public Properties

      public ButtonStates ButtonState { get; set; }

      public int WindowLeft { get; set; }

      public int WindowTop { get; set; }

      #endregion
   }
}