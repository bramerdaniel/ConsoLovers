// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingStyle.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

using ConsoLovers.ConsoleToolkit.Core;

public sealed class RenderingStyle
{
   #region Constants and Fields

   private static RenderingStyle defaultStyle;

   #endregion

   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="RenderingStyle"/> class.</summary>
   /// <param name="foreground">The foreground color.</param>
   /// <param name="background">The background color.</param>
   public RenderingStyle(ConsoleColor? foreground = null, ConsoleColor? background = null)
   {
      Foreground = foreground ?? Default.Foreground;
      Background = background ?? Default.Background;
   }

   #endregion

   #region Public Properties

   public static RenderingStyle Default => defaultStyle ??= new RenderingStyle(Console.ForegroundColor, Console.BackgroundColor);

   /// <summary>Gets the background color.</summary>
   public ConsoleColor Background { get; }

   /// <summary>Gets the foreground color.</summary>
   public ConsoleColor Foreground { get; }

   #endregion

   #region Public Methods and Operators

   public static RenderingStyle InitializeDefaultStyle(IConsole console)
   {
      defaultStyle = new RenderingStyle(console.ForegroundColor, console.BackgroundColor);
      return defaultStyle;
   }

   /// <summary>Creates a copy of the current style and adjusts the <see cref="Background"/></summary>
   /// <param name="background">The background to use .</param>
   /// <returns>A copy with the adjusted background</returns>
   public RenderingStyle WithBackground(ConsoleColor background)
   {
      return new RenderingStyle(Foreground, background);
   }

   /// <summary>Creates a copy of the current style and adjusts the <see cref="Foreground"/></summary>
   /// <param name="foreground">The foreground to use .</param>
   /// <returns>A copy with the adjusted foreground</returns>
   public RenderingStyle WithForeground(ConsoleColor foreground)
   {
      return new RenderingStyle(foreground, Background);
   }

   #endregion
}