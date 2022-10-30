// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringBuilderConsole.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests;

using System;
using System.Text;

using ConsoLovers.ConsoleToolkit.Core;

using JetBrains.Annotations;

public class StringBuilderConsole : IConsole
{
   private int cursorLeft;

   #region Constructors and Destructors

   public StringBuilderConsole()
      : this(new StringBuilder())
   {
   }

   public StringBuilderConsole([NotNull] StringBuilder content)
   {
      this.Content = content ?? throw new ArgumentNullException(nameof(content));
   }

   #endregion

   #region IConsole Members

   public void Clear()
   {
      throw new NotImplementedException();
   }

   public void Clear(ConsoleColor color)
   {
      throw new NotImplementedException();
   }

   public ConsoleKeyInfo ReadKey()
   {
      throw new NotImplementedException();
   }

   public ConsoleKeyInfo ReadKey(bool intercept)
   {
      throw new NotImplementedException();
   }

   public string ReadLine()
   {
      throw new NotImplementedException();
   }

   public void ResetColor()
   {
      throw new NotImplementedException();
   }

   public void SetCursorPosition(int left, int top)
   {
      throw new NotImplementedException();
   }

   public void Write(string value)
   {
      throw new NotImplementedException();
   }

   public void Write(char value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine()
   {
      Content.AppendLine();
      CursorTop += 1;
   }

   public void WriteLine(string value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(bool value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(char value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(char[] value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(decimal value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(double value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(float value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(int value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(long value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(object value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(uint value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(ulong value)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(string format, object arg0)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(string format, params object[] args)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(char[] buffer, int index, int count)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(string format, object arg0, object arg1)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(string format, object arg0, object arg1, object arg2)
   {
      throw new NotImplementedException();
   }

   public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
   {
      throw new NotImplementedException();
   }

   public void Write(string value, ConsoleColor foreground, ConsoleColor background)
   {
      Content.Append(value);
      cursorLeft += value.Length;
   }

   public void Write(string value, ConsoleColor foreground)
   {
      Content.Append(value);
      cursorLeft += value.Length;
   }

   public void WriteLine(string value, ConsoleColor foreground)
   {
      Content.AppendLine(value);
      cursorLeft += value.Length;
   }

   public void WriteLine(string value, ConsoleColor foreground, ConsoleColor background)
   {
      Content.AppendLine(value);
      cursorLeft += value.Length;
   }

   public void Beep()
   {
      throw new NotImplementedException();
   }

   public void WaitForKey(ConsoleKey key)
   {
      throw new NotImplementedException();
   }

   public ConsoleColor BackgroundColor { get; set; }

   public int CursorLeft
   {
      get => cursorLeft;
      set
      {
         var oldValue = cursorLeft;
         cursorLeft = value;

         var movedAmmount = cursorLeft - oldValue;
         if (movedAmmount > 0)
            Content.Append(string.Empty.PadRight(movedAmmount));
      }
   }

   public int CursorTop { get; set; }

   public ConsoleColor ForegroundColor { get; set; }

   public int WindowWidth { get; set; } = 120;

   public int WindowHeight { get; set; } = 30;

   public int LargestWindowHeight { get; set; } = 63;

   public int LargestWindowWidth { get; set; } = 240;

   public int CursorSize { get; set; }

   #endregion

   #region Public Properties

   /// <summary>Gets the content.</summary>
   /// <value>The content.</value>
   public StringBuilder Content { get; }

   #endregion
}