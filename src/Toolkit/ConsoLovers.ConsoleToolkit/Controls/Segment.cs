// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Drawable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Diagnostics;

using JetBrains.Annotations;

[DebuggerDisplay("{Text} {Style.Foreground}")]
public struct Segment
{
   #region Constructors and Destructors
   
   public Segment([NotNull] IRenderable renderable, [NotNull] string text, [NotNull] RenderingStyle style)
   {
      Renderable = renderable ?? throw new ArgumentNullException(nameof(renderable));
      Text = text ?? throw new ArgumentNullException(nameof(text));
      Style = style ?? throw new ArgumentNullException(nameof(style));
   }

   #endregion

   #region Public Properties

   public RenderingStyle Style { get; }

   /// <summary>Gets the renderable that produced this segment.</summary>
   public IRenderable Renderable { get; }

   public string Text { get; }

   public int Width => Text.Length;

   public Segment WithStyle([NotNull] RenderingStyle stye)
   {
      if (stye == null)
         throw new ArgumentNullException(nameof(stye));

      return new Segment(Renderable, Text, stye);
   }

   #endregion
}