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
   private static RenderingStyle defaultStyle, selection;

   public static RenderingStyle Default => defaultStyle ??= new RenderingStyle(Console.ForegroundColor, Console.BackgroundColor);

   public static RenderingStyle Selection => selection ??= new RenderingStyle(Console.BackgroundColor, Console.ForegroundColor);

   /// <summary>Gets the foreground color.</summary>
   public ConsoleColor Foreground { get; }

   /// <summary>
   /// Gets the background color.
   /// </summary>
   public ConsoleColor Background { get; }
   
   /// <summary>Initializes a new instance of the <see cref="RenderingStyle"/> class.</summary>
   /// <param name="foreground">The foreground color.</param>
   /// <param name="background">The background color.</param>
   public RenderingStyle(ConsoleColor? foreground = null, ConsoleColor? background = null)
   {
      Foreground = foreground ?? Default.Foreground;
      Background = background ?? Default.Background;
   }

   public static RenderingStyle InitializeDefaultStyle(IConsole console)
   {
      defaultStyle = new RenderingStyle(console.ForegroundColor, console.BackgroundColor);
      return defaultStyle;
   }
}