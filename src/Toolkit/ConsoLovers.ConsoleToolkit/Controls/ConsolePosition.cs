// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsolePosition.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using ConsoLovers.ConsoleToolkit.Core;

internal class ConsolePosition
{
   public static ConsolePosition FromConsole(IConsole console)
   {
      return new ConsolePosition(console.CursorTop, console.CursorLeft);
   }

   public ConsolePosition(int cursorTop, int cursorLeft)
   {
      CursorLeft = cursorLeft;
      CursorTop = cursorTop;
   }

   public int CursorLeft { get; }

   public int CursorTop { get; }

   public void ApplyTo(IConsole console)
   {
      console.CursorTop = CursorTop;
      console.CursorLeft = CursorLeft;
   }
}