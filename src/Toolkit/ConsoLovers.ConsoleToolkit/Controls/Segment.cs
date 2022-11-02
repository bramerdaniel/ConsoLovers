// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Drawable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Diagnostics;

using JetBrains.Annotations;

[DebuggerDisplay("{Text} {Style.GetForeground()}")]
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

   public Segment OverrideStyle([NotNull] RenderingStyle stye)
   {
      if (stye == null)
         throw new ArgumentNullException(nameof(stye));

      var foreground = stye.GetForeground(Style.GetForeground());
      var background = stye.GetBackground(Style.GetBackground());

      return new Segment(Renderable, Text, new RenderingStyle(foreground, background));
   }

   public Segment OverrideStyle(ConsoleColor? foreground, ConsoleColor? background)
   {
      var stye = new RenderingStyle(foreground ?? Style.GetForeground(), background ?? Style.GetBackground());
      return new Segment(Renderable, Text, stye);
   }

   /// <summary>Duplicates the current segment and changes the foreground color.</summary>
   /// <param name="foreground">The foreground to use.</param>
   /// <returns>A copied segment</returns>
   public Segment WithForeground([NotNull] ConsoleColor foreground)
   {
      return new Segment(Renderable, Text, Style.WithForeground(foreground));
   }

   /// <summary>Duplicates the current segment and changes the background color.</summary>
   /// <param name="background">The background to use.</param>
   /// <returns>A copied segment</returns>
   public Segment WithBackground([NotNull] ConsoleColor background)
   {
      return new Segment(Renderable, Text, Style.WithBackground(background));
   }

   #endregion
}