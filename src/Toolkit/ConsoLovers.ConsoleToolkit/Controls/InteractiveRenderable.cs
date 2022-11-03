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

   public event EventHandler<InvalidationEventArgs> Invalidated;

   #endregion

   #region IInteractiveRenderable Members

   /// <summary>Gets the children of the <see cref="IRenderable"/>.</summary>
   /// <returns>All children</returns>
   public virtual IEnumerable<IRenderable> GetChildren()
   {
      yield break;
   }

   public RenderSize Measure(int availableWidth)
   {
      MeasuredSize = MeasureOverride(availableWidth);
      return MeasuredSize;
   }

   public abstract IEnumerable<Segment> RenderLine(IRenderContext context, int line);

   public RenderingStyle Style
   {
      get => style;
      set
      {
         style = value;
         NotifyStyleChanged();
      }
   }

   #endregion

   #region Public Properties

   /// <summary>Gets the size the renderable measured for itself.</summary>
   public RenderSize MeasuredSize { get; private set; }

   #endregion

   #region Public Methods and Operators

   public abstract RenderSize MeasureOverride(int availableWidth);

   #endregion

   #region Methods

   protected virtual void Invalidate(InvalidationScope scope)
   {
      Invalidated?.Invoke(this, new InvalidationEventArgs(scope));
   }

   protected virtual void Invalidate()
   {
      Invalidate(InvalidationScope.All);
   }

   protected virtual void NotifyStyleChanged()
   {
      Invalidate(InvalidationScope.Style);
   }

   #endregion
}