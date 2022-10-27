// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Drawable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

using System;

using JetBrains.Annotations;

public struct Segment
{
   #region Constructors and Destructors

   public Segment([NotNull] string text)
      : this(text, RenderingStyle.Default)
   {
   }

   public Segment([NotNull] string text, [NotNull] RenderingStyle style)
   {
      Text = text ?? throw new ArgumentNullException(nameof(text));
      Style = style ?? throw new ArgumentNullException(nameof(style));
   }

   #endregion

   #region Public Properties

   public RenderingStyle Style { get; }

   public string Text { get; }

   public int Width => Text.Length;

   #endregion
}