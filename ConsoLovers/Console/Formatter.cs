// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Formatter.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Console
{
   using System.Drawing;

   /// <summary>
   ///    Exposes properties representing an object and its color.  This is a convenience wrapper around the StyleClass type, so you don't have to provide the type argument each
   ///    time.
   /// </summary>
   public sealed class Formatter
   {
      #region Constants and Fields

      private readonly StyleClass<object> backingClass;

      #endregion

      #region Constructors and Destructors

      /// <summary>
      ///    Exposes properties representing an object and its color.  This is a convenience wrapper around the StyleClass type, so you don't have to provide the type argument each
      ///    time.
      /// </summary>
      /// <param name="target">The object to be styled.</param>
      /// <param name="color">The color to be applied to the target.</param>
      public Formatter(object target, Color color)
      {
         backingClass = new StyleClass<object>(target, color);
      }

      #endregion

      #region Public Properties

      /// <summary>The color to be applied to the target.</summary>
      public Color Color => backingClass.Color;

      /// <summary>The object to be styled.</summary>
      public object Target => backingClass.Target;

      #endregion
   }
}