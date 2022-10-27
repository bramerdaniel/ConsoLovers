// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Line.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts
{
   using System;
   using System.Collections.Generic;

   using JetBrains.Annotations;

   public class Line : Renderable
   {
      public Line()
      {
         elements = new();
      }

      public Line([NotNull] params IRenderable[] elements)
      {
         if (elements == null)
            throw new ArgumentNullException(nameof(elements));

         this.elements = new List<IRenderable>(elements);
      }

      #region Constants and Fields

      private readonly List<IRenderable> elements;

      #endregion

      #region Public Methods and Operators

      public void Add([NotNull] IRenderable element)
      {
         if (element == null)
            throw new ArgumentNullException(nameof(element));
         elements.Add(element);
      }

      public override Measurement Measure(int maxWidth)
      {
         var measurement = new Measurement(0, maxWidth);
         foreach (var element in elements)
         {
            var measure = element.Measure(maxWidth);
            measurement = new Measurement();
         }

         return measurement;
      }

      public override void Render(IRenderContext context)
      {
         foreach (var element in elements)
            element.Render(context);

         context.Console.WriteLine();
      }

      #endregion
   }
}