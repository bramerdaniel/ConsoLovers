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