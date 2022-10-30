// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Renderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   using System;
   using System.Collections.Generic;

   using JetBrains.Annotations;

   public abstract class Renderable : IRenderable
   {
      #region Constants and Fields

      private RenderingStyle style;

      #endregion

      #region Constructors and Destructors

      protected Renderable()
         : this(RenderingStyle.Default)
      {
      }

      protected Renderable([NotNull] RenderingStyle style)
      {
         this.style = style ?? throw new ArgumentNullException(nameof(style));
      }

      #endregion

      #region IRenderable Members

      public abstract MeasuredSize Measure(int availableWidth);

      public abstract IEnumerable<Segment> RenderLine(IRenderContext context, int line);
      
      public RenderingStyle Style
      {
         get => style;
         set => style = value;
      }

      
      #endregion
   }
}