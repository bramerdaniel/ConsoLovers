﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   using System.Collections.Generic;

   public interface IRenderable
   {
      #region Public Methods and Operators

      /// <summary>Measures the renderable object.</summary>
      /// <param name="availableWidth">The maximum allowed width.</param>
      /// <returns>The minimum and maximum width of the object.</returns>
      MeasuredSize Measure(int availableWidth);
      
      IEnumerable<Segment> RenderLine(IRenderContext context, int line);
      

      /// <summary>Gets or sets the style the renderable will use.</summary>
      RenderingStyle Style { get; set; }

      #endregion
      
   }
}