// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackFrameRenderingStyles.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

public class StackFrameRenderingStyles
{
   public RenderingStyle NormalText { get; set; } = new(ConsoleColor.Gray);

   public RenderingStyle ControlCharacters { get; set; } = new(ConsoleColor.DarkGray);
   
   public RenderingStyle ParameterName { get; set; } = new(ConsoleColor.Gray);

   public RenderingStyle FilePath { get; set; } = new(ConsoleColor.DarkYellow);
   
   public RenderingStyle MethodName { get; set; } = new(ConsoleColor.DarkYellow);
   
   public RenderingStyle FileName { get; set; } = new(ConsoleColor.DarkYellow);
   
   public RenderingStyle LineNumber { get; set; } = new(ConsoleColor.Blue);
   
   public RenderingStyle Namespaces { get; set; } = new(ConsoleColor.Gray);

   public RenderingStyle Types { get; set; } = new(ConsoleColor.DarkCyan);
   
   public RenderingStyle MouseOver { get; set; } = new(null, ConsoleColor.DarkGray);
}