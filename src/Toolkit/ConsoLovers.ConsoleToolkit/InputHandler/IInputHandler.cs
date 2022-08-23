namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;

   public interface IInputHandler
   {
      #region Public Events

      /// <summary>Occurs when a keyboard key was pressed.</summary>
      event EventHandler<KeyEventArgs> KeyDown;

      /// <summary>Occurs when a mouse button was clicked.</summary>
      event EventHandler<MouseEventArgs> MouseClicked;

      /// <summary>Occurs when a mouse button was double clicked.</summary>
      event EventHandler<MouseEventArgs> MouseDoubleClicked;

      /// <summary>Occurs when the mouse was moved.</summary>
      event EventHandler<MouseEventArgs> MouseMoved;

      /// <summary>Occurs when the mouse wheel position changed.</summary>
      event EventHandler<MouseEventArgs> MouseWheelChanged;

      #endregion

      #region Public Methods and Operators

      /// <summary>Starts the input observation.</summary>
      void Start();

      /// <summary>Stops the input observation.</summary>
      void Stop();

      /// <summary>Joins the calling and the input handler thread.</summary>
      void Wait();

      #endregion
   }
}