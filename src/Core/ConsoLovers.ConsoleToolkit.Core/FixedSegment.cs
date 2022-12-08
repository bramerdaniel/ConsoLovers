// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedSegment.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

/// <summary>The implementation of the <see cref="IFixedSegment"/></summary>
/// <seealso cref="IFixedSegment"/>
internal class FixedSegment : IFixedSegment
{
   private readonly IConsole console;

   private readonly int initialTop;

   private readonly int initialLeft;

   private ConsoleColor foreground;

   private string text;

   private ConsoleColor background;

   private int width;

#if NET6_0_OR_GREATER || NETFRAMEWORK
   private readonly ConsoleBuffer buffer = new();
#endif

   public FixedSegment([NotNull] IConsole console, int left, int top, int width)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      initialLeft = ValidateLeft(left);
      initialTop = ValidateTop(top);

      this.width = width;

      foreground = console.ForegroundColor;
      background = console.BackgroundColor;
   }

   private int ValidateTop(int top)
   {
      if (top < 0)
         throw new ArgumentOutOfRangeException(nameof(top), "Top must be greater than zero");
      if (top > console.WindowHeight)
         throw new ArgumentOutOfRangeException(nameof(top), "Top must be smaller than the window height");

      return top;
   }

   private int ValidateLeft(int left)
   {
      if (left < 0)
         throw new ArgumentOutOfRangeException(nameof(left), "Left must be greater than zero");
      if (left > console.WindowWidth)
         throw new ArgumentOutOfRangeException(nameof(left), "Left must be smaller than the window width");
      return left;
   }

   public int Width
   {
      get => width;
      set
      {
         if (width == value)
            return;

         width = value;
         RenderSegment();
         RaisePropertyChanged();
      }
   }

   public string Text
   {
      get => text;
      set
      {
         if (text == value)
            return;

         text = TrimValue(value);
         RenderSegment();
         RaisePropertyChanged();
      }
   }

   public ConsoleColor Foreground
   {
      get => foreground;
      set
      {
         if (foreground == value)
            return;

         foreground = value;
         RenderSegment();
         RaisePropertyChanged();
      }
   }

   public ConsoleColor Background
   {
      get => background;
      set
      {
         if (background == value)
            return;

         background = value;
         RenderSegment();
         RaisePropertyChanged();
      }
   }

   public IFixedSegment Update(string value, ConsoleColor newForeground, ConsoleColor newBackground)
   {
      var trimmedValue = TrimValue(value);
      var textChanged = string.Equals(text, trimmedValue);
      text = trimmedValue;

      var foregroundChanged = newForeground != foreground;
      foreground = newForeground;

      var backgroundChanged = newBackground != background;
      background = newBackground;

      RenderSegment();

      if (textChanged)
         RaisePropertyChanged(nameof(Text));

      if (foregroundChanged)
         RaisePropertyChanged(nameof(Foreground));

      if (backgroundChanged)
         RaisePropertyChanged(nameof(Background));

      return this;
   }

   public IFixedSegment Update(string value, ConsoleColor newForeground)
   {
      var trimmedValue = TrimValue(value);
      var textChanged = string.Equals(text, trimmedValue);
      text = trimmedValue;

      var foregroundChanged = newForeground != foreground;
      foreground = newForeground;

      RenderSegment();

      if (textChanged)
         RaisePropertyChanged(nameof(Text));

      if (foregroundChanged)
         RaisePropertyChanged(nameof(Foreground));

      return this;
   }

   public IFixedSegment Update(string value)
   {
      Text = value;
      return this;
   }

   private string TrimValue(string value)
   {
      return value.Length > Width
         ? value.Substring(0, Width)
         : value;
   }

   private void RenderSegment()
   {
#if NETFRAMEWORK
      UpdateWithBuffer(Text);
      return;
#endif

#if NET6_0_OR_GREATER
      if (OperatingSystem.IsWindows())
      {
         UpdateWithBuffer(Text);
         return;
      }
#endif
      UpdateNormal(Text);
   }

   private void UpdateNormal(string value)
   {
      var top = console.CursorTop;
      var left = console.CursorLeft;

      console.CursorTop = initialTop;
      console.CursorLeft = initialLeft;
      console.Write(value.PadRight(Width), Foreground, Background);

      console.CursorTop = top;
      console.CursorLeft = left;
   }

#if NET6_0_OR_GREATER || NETFRAMEWORK
   private void UpdateWithBuffer(string value)
   {
      buffer.WriteLine(initialLeft, initialTop, value.PadRight(Width), Foreground, Background, false);
   }
#endif
   public event PropertyChangedEventHandler PropertyChanged;

   [NotifyPropertyChangedInvocator]
   protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}