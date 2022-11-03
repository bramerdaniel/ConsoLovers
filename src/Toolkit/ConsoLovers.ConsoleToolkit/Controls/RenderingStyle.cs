// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingStyle.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Diagnostics;

using ConsoLovers.ConsoleToolkit.Core;

[DebuggerDisplay("{foreground}, {background}")]
public sealed class RenderingStyle
{
   #region Constants and Fields

   private readonly ConsoleColor? background;

   private readonly ConsoleColor? foreground;

   private static RenderingStyle defaultStyle;

   #endregion

   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="RenderingStyle"/> class.</summary>
   /// <param name="foreground">The foreground color.</param>
   /// <param name="background">The background color.</param>
   public RenderingStyle(ConsoleColor? foreground = null, ConsoleColor? background = null)
   {
      this.foreground = foreground;
      this.background = background;
   }

   #endregion

   #region Public Properties

   public static RenderingStyle Default => defaultStyle ??= new RenderingStyle(Console.ForegroundColor, Console.BackgroundColor);

   public ConsoleColor GetForeground(ConsoleColor defaultValue) => foreground ?? defaultValue;

   public ConsoleColor GetForeground() => foreground ?? Console.ForegroundColor;

   public ConsoleColor GetBackground(ConsoleColor defaultValue) => background ?? defaultValue;
   
   public ConsoleColor GetBackground() => background ?? Console.BackgroundColor;

   #endregion

   #region Public Methods and Operators

   public static RenderingStyle InitializeDefaultStyle(IConsole console)
   {
      defaultStyle = new RenderingStyle(console.ForegroundColor, console.BackgroundColor);
      return defaultStyle;
   }

   /// <summary>Creates a copy of the current style and adjusts the <see cref="Background"/></summary>
   /// <param name="backgroundColor">The background to use .</param>
   /// <returns>A copy with the adjusted background</returns>
   public RenderingStyle WithBackground(ConsoleColor backgroundColor)
   {
      return new RenderingStyle(foreground, backgroundColor);
   }

   /// <summary>Creates a copy of the current style and adjusts the <see cref="Foreground"/></summary>
   /// <param name="foregroundColor">The foreground to use .</param>
   /// <returns>A copy with the adjusted foreground</returns>
   public RenderingStyle WithForeground(ConsoleColor foregroundColor)
   {
      return new RenderingStyle(foregroundColor, background);
   }

   #endregion
}