﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Renderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;

   using JetBrains.Annotations;

   public abstract class Renderable : IRenderable
   {
      #region Constructors and Destructors

      protected Renderable()
         : this(RenderingStyle.Default)
      {
      }

      protected Renderable([NotNull] RenderingStyle style)
      {
         Style = style ?? throw new ArgumentNullException(nameof(style));
      }

      #endregion

      #region IRenderable Members

      [DebuggerStepThrough]
      public RenderSize Measure(int availableWidth)
      {
         MeasuredSize = MeasureOverride(availableWidth);
         return MeasuredSize;
      }
      
      protected bool IsLastLine(int line)
      {
         return MeasuredSize.Height - 1 == line;
      }

      public abstract IEnumerable<Segment> RenderLine(IRenderContext context, int line);

      /// <summary>Gets or sets the style the renderable will use.</summary>
      public RenderingStyle Style { get; set; }

      #endregion

      #region Public Properties

      public RenderSize MeasuredSize { get; private set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>The measure override for the renderable.</summary>
      /// <param name="availableWidth">Width of the available.</param>
      /// <returns>The computed <see cref="RenderSize"/></returns>
      public abstract RenderSize MeasureOverride(int availableWidth);

      #endregion
   }
}