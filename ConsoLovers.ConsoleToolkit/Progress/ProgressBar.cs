// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBar.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Progress
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading;

   public class ProgressBar : ProgressBarBase, IProgressBar
   {
      #region Constants and Fields

      private static readonly object Lock = new object();

      private readonly ConsoleColor _originalColor;

      private readonly int _originalCursorTop;

      private readonly int _originalWindowTop;

      private bool _isDisposed;

      private Timer timer;

      private int _visisbleDescendants;

      #endregion

      #region Constructors and Destructors

      public ProgressBar(int maxTicks, string message, ConsoleColor color)
         : this(maxTicks, message, new ProgressBarOptions { ForeGroundColor = color })
      {
      }

      public ProgressBar(int maxTicks, string message, ProgressBarOptions options = null)
         : base(maxTicks, message, options)
      {
         _originalCursorTop = Console.CursorTop;
         _originalWindowTop = Console.WindowTop;
         _originalColor = Console.ForegroundColor;

         Console.CursorVisible = false;

         if (Options.DisplayTimeInRealTime)
            timer = new Timer((s) => DisplayProgress(), null, 500, 500);
      }

      #endregion

      #region IDisposable Members

      public void Dispose()
      {
         if (EndTime == null)
            EndTime = DateTime.Now;
         var openDescendantsPadding = (_visisbleDescendants * 2);

         try
         {
            int moveDown;
            var currentWindowTop = Console.WindowTop;
            if (currentWindowTop != _originalWindowTop)
            {
               var x = Math.Max(0, Math.Min(2, currentWindowTop - _originalWindowTop));
               moveDown = _originalCursorTop + x;
            }
            else
               moveDown = _originalCursorTop + 2;

            Console.CursorVisible = true;
            Console.CursorLeft = 0;
            Console.CursorTop = (openDescendantsPadding + moveDown);
         }
         // This is bad and I should feel bad, but i rather eat pbar exceptions in productions then causing false negatives
         catch
         {
         }
         Console.WriteLine();
         _isDisposed = true;
         timer?.Dispose();
         timer = null;
         foreach (var c in Children)
            c.Dispose();
      }

      #endregion

      #region Methods

      protected override void DisplayProgress()
      {
         if (_isDisposed)
            return;

         var indentation = new[] { new Indentation(ForeGroundColor, true) };
         var mainPercentage = Percentage;

         lock (Lock)
         {
            Console.ForegroundColor = ForeGroundColor;

            if (Options.ProgressBarOnBottom)
            {
               Console.CursorLeft = 0;
               ProgressBarBottomHalf(mainPercentage, _startDate, null, Message, indentation, Options.ProgressBarOnBottom);

               Console.CursorLeft = 0;
               ProgressBarTopHalf(mainPercentage, Options.ProgressCharacter, Options.BackgroundColor, indentation, Options.ProgressBarOnBottom);
            }
            else
            {
               Console.CursorLeft = 0;
               ProgressBarTopHalf(mainPercentage, Options.ProgressCharacter, Options.BackgroundColor, indentation, Options.ProgressBarOnBottom);

               Console.CursorLeft = 0;
               ProgressBarBottomHalf(mainPercentage, _startDate, null, Message, indentation, Options.ProgressBarOnBottom);
            }

            DrawChildren(Children, indentation);

            ResetToBottom();

            Console.CursorLeft = 0;
            Console.WindowTop = _originalWindowTop;
            Console.CursorTop = _originalCursorTop;
            Console.ForegroundColor = _originalColor;

            if (mainPercentage >= 100)
            {
               timer?.Dispose();
               timer = null;
            }
         }
      }

      protected override void Grow(ProgressBarHeight direction)
      {
         switch (direction)
         {
            case ProgressBarHeight.Increment:
               Interlocked.Increment(ref _visisbleDescendants);
               break;
            case ProgressBarHeight.Decrement:
               Interlocked.Decrement(ref _visisbleDescendants);
               break;
         }
      }

      private static void DrawBottomHalfPrefix(Indentation[] indentation, int depth)
      {
         for (var i = 1; i < depth; i++)
         {
            var ind = indentation[i];
            Console.ForegroundColor = indentation[i - 1].ConsoleColor;
            if (!ind.LastChild)
               Console.Write(i == (depth - 1) ? ind.Glyph : "│ ");
            else
               Console.Write(i == (depth - 1) ? ind.Glyph : "  ");
         }
         Console.ForegroundColor = indentation[depth - 1].ConsoleColor;
      }

      private static void DrawChildren(IEnumerable<ChildProgressBar> children, Indentation[] indentation)
      {
         var view = children.Where(c => !c.Collapse).Select((c, i) => new { c, i }).ToList();
         if (!view.Any())
            return;

         var lastChild = view.Max(t => t.i);
         foreach (var tuple in view)
         {
            if (Console.CursorTop >= (Console.WindowHeight - 2))
               return;

            var child = tuple.c;
            var currentIndentation = new Indentation(child.ForeGroundColor, tuple.i == lastChild);
            var childIndentation = NewIndentation(indentation, currentIndentation);

            var percentage = child.Percentage;
            Console.ForegroundColor = child.ForeGroundColor;

            if (child.Options.ProgressBarOnBottom)
            {
               Console.CursorLeft = 0;
               ProgressBarBottomHalf(percentage, child.StartDate, child.EndTime, child.Message, childIndentation, child.Options.ProgressBarOnBottom);

               Console.CursorLeft = 0;
               ProgressBarTopHalf(percentage, child.Options.ProgressCharacter, child.Options.BackgroundColor, childIndentation, child.Options.ProgressBarOnBottom);
            }
            else
            {
               Console.CursorLeft = 0;
               ProgressBarTopHalf(percentage, child.Options.ProgressCharacter, child.Options.BackgroundColor, childIndentation, child.Options.ProgressBarOnBottom);

               Console.CursorLeft = 0;
               ProgressBarBottomHalf(percentage, child.StartDate, child.EndTime, child.Message, childIndentation, child.Options.ProgressBarOnBottom);
            }

            DrawChildren(child.Children, childIndentation);
         }
      }

      private static void DrawTopHalfPrefix(Indentation[] indentation, int depth)
      {
         for (var i = 1; i < depth; i++)
         {
            var ind = indentation[i];
            Console.ForegroundColor = indentation[i - 1].ConsoleColor;
            if (ind.LastChild && i != (depth - 1))
               Console.Write("  ");
            else
               Console.Write("│ ");
         }
         Console.ForegroundColor = indentation[depth - 1].ConsoleColor;
      }

      private static Indentation[] NewIndentation(Indentation[] array, Indentation append)
      {
         var result = new Indentation[array.Length + 1];
         Array.Copy(array, result, array.Length);
         result[array.Length] = append;
         return result;
      }

      private static void ProgressBarBottomHalf(double percentage, DateTime startDate, DateTime? endDate, string message, Indentation[] indentation, bool progressBarOnTop)
      {
         var depth = indentation.Length;
         var maxCharacterWidth = Console.WindowWidth - (depth * 2) + 2;
         var duration = ((endDate ?? DateTime.Now) - startDate);
         var durationString = $"{duration.Hours:00}:{duration.Minutes:00}:{duration.Seconds:00}";

         var column1Width = Console.WindowWidth - durationString.Length - (depth * 2) + 2;
         var column2Width = durationString.Length;

         if (progressBarOnTop)
            DrawTopHalfPrefix(indentation, depth);
         else
            DrawBottomHalfPrefix(indentation, depth);

         var format = $"{{0, -{column1Width}}}{{1,{column2Width}}}";

         var truncatedMessage = StringExtensions.Excerpt($"{percentage:N2}%" + " " + message, column1Width);
         var formatted = string.Format(format, truncatedMessage, durationString);
         var m = formatted + new string(' ', Math.Max(0, maxCharacterWidth - formatted.Length));
         Console.Write(m);
      }

      private static void ProgressBarTopHalf(double percentage, char progressCharacter, ConsoleColor? backgroundColor, Indentation[] indentation, bool progressBarOnTop)
      {
         var depth = indentation.Length;
         var width = Console.WindowWidth - (depth * 2) + 2;

         if (progressBarOnTop)
            DrawBottomHalfPrefix(indentation, depth);
         else
            DrawTopHalfPrefix(indentation, depth);

         var newWidth = (int)((width * percentage) / 100d);
         var progBar = new string(progressCharacter, newWidth);
         Console.Write(progBar);
         if (backgroundColor.HasValue)
         {
            Console.ForegroundColor = backgroundColor.Value;
            Console.Write(new string(progressCharacter, width - newWidth));
         }
         else
            Console.Write(new string(' ', width - newWidth));
         Console.ForegroundColor = indentation[depth - 1].ConsoleColor;
      }

      private static string ResetString() => new string(' ', Console.WindowWidth);

      private static void ResetToBottom()
      {
         if (Console.CursorTop >= (Console.WindowHeight - 1))
            return;
         do
         {
            Console.Write(ResetString());
         }
         while (Console.CursorTop < (Console.WindowHeight - 1));
      }

      #endregion

      private struct Indentation
      {
         #region Constants and Fields

         public readonly ConsoleColor ConsoleColor;

         public readonly bool LastChild;

         #endregion

         #region Constructors and Destructors

         public Indentation(ConsoleColor color, bool lastChild)
         {
            ConsoleColor = color;
            LastChild = lastChild;
         }

         #endregion

         #region Public Properties

         public string Glyph => !LastChild ? "├─" : "└─";

         #endregion
      }
   }
}