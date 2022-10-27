// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingStyle.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

using System;

using ConsoLovers.ConsoleToolkit.Core;

public sealed class RenderingStyle 
{
   public static RenderingStyle Default { get; private set; }

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

   public static void InitializeDefaultStyle(IConsole console)
   {
      Default =new RenderingStyle(console.ForegroundColor, console.BackgroundColor);
   }
}