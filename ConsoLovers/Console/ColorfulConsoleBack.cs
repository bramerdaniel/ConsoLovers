// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorfulConsoleBack.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Collections.Concurrent;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;

   /// <summary>Wraps around the System.ColoredConsole class, adding enhanced styling functionality.</summary>
   public partial class ColoredConsole
   {
      #region Constants and Fields

      // Note that if you set ConsoleColor.Black to a different color, then the background of the
      // console will change as a side-effect!  The index of Black (in the ConsoleColor definition) is 0,
      // so avoid that index.
      private const int INITIAL_COLOR_CHANGE_COUNT_VALUE = 1;

      // Limitation of the Windows console window.
      private const int MAX_COLOR_CHANGES = 16;

      private static readonly Color blackEquivalent = Color.FromArgb(0, 0, 0);

      private static readonly Color blueEquivalent = Color.FromArgb(0, 0, 255);                          

      private static readonly Color cyanEquivalent = Color.FromArgb(0, 255, 255);

      private static readonly Color darkBlueEquivalent = Color.FromArgb(0, 0, 128);

      private static readonly Color darkCyanEquivalent = Color.FromArgb(0, 128, 128);

      private static readonly Color darkGrayEquivalent = Color.FromArgb(128, 128, 128);

      private static readonly Color darkGreenEquivalent = Color.FromArgb(0, 128, 0);

      private static readonly Color darkMagentaEquivalent = Color.FromArgb(128, 0, 128);

      private static readonly Color darkRedEquivalent = Color.FromArgb(128, 0, 0);

      private static readonly Color darkYellowEquivalent = Color.FromArgb(128, 128, 0);

      private static readonly Color grayEquivalent = Color.FromArgb(192, 192, 192);

      private static readonly Color greenEquivalent = Color.FromArgb(0, 255, 0);

      private static readonly Color magentaEquivalent = Color.FromArgb(255, 0, 255);

      private static readonly Color redEquivalent = Color.FromArgb(255, 0, 0);

      private static readonly Color whiteEquivalent = Color.FromArgb(255, 255, 255);

      private static readonly string WRITE_TRAILER = "";

      private static readonly string WRITELINE_TRAILER = "\r\n";

      private static readonly Color yellowEquivalent = Color.FromArgb(255, 255, 0);

      private readonly ColorManager colorManager;

      private readonly ColorManagerFactory colorManagerFactory;

      private readonly ColorStore colorStore;

      #endregion

      #region Methods

      private void DoWithGradient<T>(Action<object, Color> writeAction, IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient)
      {
         GradientGenerator generator = new GradientGenerator();
         List<StyleClass<T>> gradient = generator.GenerateGradient(input, startColor, endColor, maxColorsInGradient);

         foreach (StyleClass<T> item in gradient)
         {
            writeAction(item.Target, item.Color);
         }
      }

      private ColorStore GetColorStore()
      {
         ConcurrentDictionary<Color, ConsoleColor> colorMap = new ConcurrentDictionary<Color, ConsoleColor>();
         ConcurrentDictionary<ConsoleColor, Color> consoleColorMap = new ConcurrentDictionary<ConsoleColor, Color>();

         consoleColorMap.TryAdd(ConsoleColor.Black, blackEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Blue, blueEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Cyan, cyanEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkBlue, darkBlueEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkCyan, darkCyanEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkGray, darkGrayEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkGreen, darkGreenEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkMagenta, darkMagentaEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkRed, darkRedEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.DarkYellow, darkYellowEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Gray, grayEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Green, greenEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Magenta, magentaEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Red, redEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.White, whiteEquivalent);
         consoleColorMap.TryAdd(ConsoleColor.Yellow, yellowEquivalent);

         return new ColorStore(colorMap, consoleColorMap);
      }

      private Figlet GetFiglet(FigletFont font = null)
      {
         return font == null ? new Figlet() : new Figlet(font);
      }

      private void PopulateColorGeometry(IEnumerable<KeyValuePair<string, Color>> annotationMap, StyledString target)
      {
         int abstractCharCount = 0;
         foreach (KeyValuePair<string, Color> fragment in annotationMap)
         {
            for (int i = 0; i < fragment.Key.Length; i++)
            {
               // This will run O(n^2) times...but with DP, could be O(n).
               // Just need to keep a third array that keeps track of each abstract char's width, so you never iterate past that.
               // This third array would be one-dimensional.

               int rowLength = target.CharacterIndexGeometry.GetLength(0);
               int columnLength = target.CharacterIndexGeometry.GetLength(1);
               for (int row = 0; row < rowLength; row++)
               {
                  for (int column = 0; column < columnLength; column++)
                  {
                     if (target.CharacterIndexGeometry[row, column] == abstractCharCount)
                     {
                        target.ColorGeometry[row, column] = fragment.Value;
                     }
                  }
               }

               abstractCharCount++;
            }
         }
      }

      private void MapToScreen(IEnumerable<KeyValuePair<string, Color>> styleMap, string trailer)
      {
         int writeCount = 1;
         var styleMapArray = styleMap as KeyValuePair<string, Color>[] ?? styleMap.ToArray();
         foreach (KeyValuePair<string, Color> textChunk in styleMapArray)
         {
            Console.ForegroundColor = colorManager.GetConsoleColor(textChunk.Value);

            if (writeCount == styleMapArray.Length)
            {
               Console.Write(textChunk.Key + trailer);
            }
            else
            {
               Console.Write(textChunk.Key);
            }

            writeCount++;
         }

         Console.ResetColor();
      }

      private void MapToScreen(StyledString styledString, string trailer)
      {
         int rowLength = styledString.CharacterGeometry.GetLength(0);
         int columnLength = styledString.CharacterGeometry.GetLength(1);
         for (int row = 0; row < rowLength; row++)
         {
            for (int column = 0; column < columnLength; column++)
            {
               Console.ForegroundColor = colorManager.GetConsoleColor(styledString.ColorGeometry[row, column]);

               if (row == rowLength - 1 && column == columnLength - 1)
               {
                  Console.Write(styledString.CharacterGeometry[row, column] + trailer);
               }
               else if (column == columnLength - 1)
               {
                  Console.Write(styledString.CharacterGeometry[row, column] + "\r\n");
               }
               else
               {
                  Console.Write(styledString.CharacterGeometry[row, column]);
               }
            }
         }

         Console.ResetColor();
      }

      private void WriteAsciiInColorStyled(string trailer, StyledString target, StyleSheet styleSheet)
      {
         TextAnnotator annotator = new TextAnnotator(styleSheet);
         List<KeyValuePair<string, Color>> annotationMap = annotator.GetAnnotationMap(target.AbstractValue); // Should eventually be target.AsStyledString() everywhere...?

         PopulateColorGeometry(annotationMap, target);

         MapToScreen(target, trailer);
      }

      private void WriteChunkInColor(Action<string> action, char[] buffer, int index, int count, Color color)
      {
         string chunk = buffer.AsString().Substring(index, count);

         WriteInColor(action, chunk, color);
      }

      private void WriteChunkInColorAlternating(Action<string> action, char[] buffer, int index, int count, ColorAlternator alternator)
      {
         string chunk = buffer.AsString().Substring(index, count);

         WriteInColorAlternating(action, chunk, alternator);
      }

      private void WriteChunkInColorStyled(string trailer, char[] buffer, int index, int count, StyleSheet styleSheet)
      {
         string chunk = buffer.AsString().Substring(index, count);

         WriteInColorStyled(trailer, chunk, styleSheet);
      }

      private void WriteInColor<T>(Action<T> action, T target, Color color)
      {
         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target);
         Console.ResetColor();
      }

      private void WriteInColor<T, U>(Action<T, U> action, T target0, U target1, Color color)
      {
         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target0, target1);
         Console.ResetColor();
      }

      private void WriteInColor<T, U>(Action<T, U, U> action, T target0, U target1, U target2, Color color)
      {
         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target0, target1, target2);
         Console.ResetColor();
      }

      private void WriteInColor<T, U>(Action<T, U, U, U> action, T target0, U target1, U target2, U target3, Color color)
      {
         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target0, target1, target2, target3);
         Console.ResetColor();
      }

      private void WriteInColorAlternating<T>(Action<T> action, T target, ColorAlternator alternator)
      {
         Color color = alternator.GetNextColor(target.AsString());

         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target);
         Console.ResetColor();
      }

      private void WriteInColorAlternating<T, U>(Action<T, U> action, T target0, U target1, ColorAlternator alternator)
      {
         string formatted = string.Format(target0.ToString(), target1.Normalize());
         Color color = alternator.GetNextColor(formatted);

         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target0, target1);
         Console.ResetColor();
      }

      private void WriteInColorAlternating<T, U>(Action<T, U, U> action, T target0, U target1, U target2, ColorAlternator alternator)
      {
         string formatted = string.Format(target0.ToString(), target1, target2); // NOT FORMATTING
         Color color = alternator.GetNextColor(formatted);

         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target0, target1, target2);
         Console.ResetColor();
      }

      private void WriteInColorAlternating<T, U>(Action<T, U, U, U> action, T target0, U target1, U target2, U target3, ColorAlternator alternator)
      {
         string formatted = string.Format(target0.ToString(), target1, target2, target3);
         Color color = alternator.GetNextColor(formatted);

         Console.ForegroundColor = colorManager.GetConsoleColor(color);
         action.Invoke(target0, target1, target2, target3);
         Console.ResetColor();
      }

      private void WriteInColorFormatted<T, U>(string trailer, T target0, U target1, Color styledColor, Color defaultColor)
      {
         TextFormatter formatter = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = formatter.GetFormatMap(target0.ToString(), target1.Normalize(), new[] { styledColor });

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorFormatted<T>(string trailer, T target0, Formatter target1, Color defaultColor)
      {
         TextFormatter formatter = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = formatter.GetFormatMap(target0.ToString(), new[] { target1.Target }, new[] { target1.Color });

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorFormatted<T, U>(string trailer, T target0, U target1, U target2, Color styledColor, Color defaultColor)
      {
         TextFormatter formatter = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = formatter.GetFormatMap(target0.ToString(), new[] { target1, target2 }.Normalize(), new[] { styledColor });

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorFormatted<T>(string trailer, T target0, Formatter target1, Formatter target2, Color defaultColor)
      {
         TextFormatter formatter = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = formatter.GetFormatMap(target0.ToString(), new[] { target1.Target, target2.Target }, new[] { target1.Color, target2.Color });

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorFormatted<T, U>(string trailer, T target0, U target1, U target2, U target3, Color styledColor, Color defaultColor)
      {
         TextFormatter formatter = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = formatter.GetFormatMap(target0.ToString(), new[] { target1, target2, target3 }.Normalize(), new[] { styledColor });

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorFormatted<T>(string trailer, T target0, Formatter target1, Formatter target2, Formatter target3, Color defaultColor)
      {
         TextFormatter styler = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = styler.GetFormatMap(
            target0.ToString(),
            new[] { target1.Target, target2.Target, target3.Target },
            new[] { target1.Color, target2.Color, target3.Color });

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorFormatted<T>(string trailer, T target0, Formatter[] targets, Color defaultColor)
      {
         TextFormatter styler = new TextFormatter(defaultColor);
         List<KeyValuePair<string, Color>> formatMap = styler.GetFormatMap(
            target0.ToString(),
            targets.Select(formatter => formatter.Target).ToArray(),
            targets.Select(formatter => formatter.Color).ToArray());

         MapToScreen(formatMap, trailer);
      }

      private void WriteInColorStyled<T>(string trailer, T target, StyleSheet styleSheet)
      {
         TextAnnotator annotator = new TextAnnotator(styleSheet);
         List<KeyValuePair<string, Color>> annotationMap = annotator.GetAnnotationMap(target.AsString());

         MapToScreen(annotationMap, trailer);
      }

      private void WriteInColorStyled<T, U>(string trailer, T target0, U target1, StyleSheet styleSheet)
      {
         TextAnnotator annotator = new TextAnnotator(styleSheet);

         string formatted = string.Format(target0.ToString(), target1.Normalize());
         List<KeyValuePair<string, Color>> annotationMap = annotator.GetAnnotationMap(formatted);

         MapToScreen(annotationMap, trailer);
      }

      private void WriteInColorStyled<T, U>(string trailer, T target0, U target1, U target2, StyleSheet styleSheet)
      {
         TextAnnotator annotator = new TextAnnotator(styleSheet);

         string formatted = string.Format(target0.ToString(), target1, target2);
         List<KeyValuePair<string, Color>> annotationMap = annotator.GetAnnotationMap(formatted);

         MapToScreen(annotationMap, trailer);
      }

      private void WriteInColorStyled<T, U>(string trailer, T target0, U target1, U target2, U target3, StyleSheet styleSheet)
      {
         TextAnnotator annotator = new TextAnnotator(styleSheet);

         string formatted = string.Format(target0.ToString(), target1, target2, target3);
         List<KeyValuePair<string, Color>> annotationMap = annotator.GetAnnotationMap(formatted);

         MapToScreen(annotationMap, trailer);
      }

      #endregion
   }
}