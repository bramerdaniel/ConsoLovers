// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Button.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.InputHandler;

public class Button : Border, IInteractiveRenderable, IMouseInputHandler, IMouseAware
{
   #region Constants and Fields

   private bool isMouseOver;

   #endregion

   #region Constructors and Destructors

   public Button(IRenderable content)
      : base(content)
   {
   }

   public Button(string text)
      : this(new Text(text))
   {
   }

   #endregion

   #region Public Events

   /// <summary>Occurs when the button is clicked.</summary>
   public event EventHandler Clicked;

   public event EventHandler<InvalidationEventArgs> Invalidated;

   #endregion

   #region IInteractiveRenderable Members

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      foreach (var segment in base.RenderLine(context, line))
      {
         if (isMouseOver)
         {
            yield return segment.OverrideStyle(DefaultStyles.MouseOverStyle)
               .WithOwner(this);
         }
         else
         {
            yield return segment.WithOwner(this);
         }
      }
   }

   #endregion

   #region IMouseAware Members

   /// <summary>Gets or sets a value indicating whether this instance is mouse over.</summary>
   /// <value><c>true</c> if this instance is mouse over; otherwise, <c>false</c>.</value>
   bool IMouseAware.IsMouseOver
   {
      get => isMouseOver;
      set
      {
         if (isMouseOver == value)
            return;

         isMouseOver = value;
         Invalidated?.Invoke(this, new InvalidationEventArgs(InvalidationScope.Style));
      }
   }

   #endregion

   #region IMouseInputHandler Members

   public void HandleMouseInput(IMouseInputContext context)
   {
      if (context.MouseEventArgs.ButtonState == ButtonStates.Left)
         Clicked?.Invoke(this, EventArgs.Empty);
   }

   #endregion
}