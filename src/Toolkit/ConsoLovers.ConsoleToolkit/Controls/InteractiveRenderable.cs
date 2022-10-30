// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InteractiveRenderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

public abstract class InteractiveRenderable : IInteractiveRenderable
{
   #region Constants and Fields

   private RenderingStyle style;

   #endregion

   #region Constructors and Destructors

   protected InteractiveRenderable()
      : this(RenderingStyle.Default)
   {
   }

   protected InteractiveRenderable([NotNull] RenderingStyle style)
   {
      this.style = style ?? throw new ArgumentNullException(nameof(style));
   }

   #endregion

   #region Public Events

   public event EventHandler Invalidated;

   #endregion

   #region IInteractiveRenderable Members

   public abstract MeasuredSize Measure(int availableWidth);

   public abstract IEnumerable<Segment> RenderLine(IRenderContext context, int line);

   public RenderingStyle Style
   {
      get => style;
      set
      {
         style = value;
         RaiseInvalidated();
      }
   }

   #endregion

   #region Methods

   protected virtual void RaiseInvalidated()
   {
      Invalidated?.Invoke(this, EventArgs.Empty);
   }

   #endregion
}