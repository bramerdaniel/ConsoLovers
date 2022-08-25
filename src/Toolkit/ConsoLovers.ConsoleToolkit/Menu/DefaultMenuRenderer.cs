// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultMenuRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;

   using JetBrains.Annotations;

   internal class DefaultMenuRenderer : IMenuRenderer
   {
      internal IConsoleMenuOptions Options { get; }

      #region Constants and Fields

      private const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;

      private const ConsoleColor DEFAULT_BACKGROUND_COLOR_MOUSE_OVER = ConsoleColor.Gray;

      private const ConsoleColor DEFAULT_FOREGROUND_COLOR = ConsoleColor.Gray;

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public DefaultMenuRenderer([NotNull] IConsole console, [NotNull] IConsoleMenuOptions options)
      {
         Options = options ?? throw new ArgumentNullException(nameof(options));
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      #endregion

      #region IMenuRenderer Members

      public void Header(object header)
      {
         if (header is string textHeader)
         {
            Print(textHeader, DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
            console.WriteLine();
         }
         else if (header is ICustomHeader customHeader)
         {
            customHeader.PrintHeader();
         }
         else if (header != null)
         {
            Print(header.ToString(), DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
         }
         else
         {
            // Ignore empty header
         }
      }

      public void Footer(object footer)
      {
         if (footer is string textFooter)
         {
            Print(textFooter, DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
            console.WriteLine();
         }
         else if (footer is ICustomFooter customFooter)
         {
            customFooter.PrintFooter();
         }
         else if (footer != null)
         {
            Print(footer.ToString(), DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
         }
         else
         {
            // Ignore empty footer
         }
      }

      public void Element(ElementInfo element, string selector, SelectionMode selectionMode)
      {
         var foreground = element.IsMouseOver
            ? ConsoleColor.Black
            : element.Foreground.GetValueOrDefault(element.Disabled ? ConsoleColor.DarkGray : element.IsSelected ? ConsoleColor.Black : ConsoleColor.Gray);

         var background = element.IsMouseOver && !element.IsSelected
            ? DEFAULT_BACKGROUND_COLOR_MOUSE_OVER
            : element.Background.GetValueOrDefault(element.IsSelected ? ConsoleColor.White : DEFAULT_BACKGROUND_COLOR);

         Selector(element, selector);

         Print(element.Indent, foreground, background);
         PrintExpander(element, foreground, background);

         if (Options.IndexMenuItems)
            Print(element.IndexString, foreground, background);

         Print(element.Text, foreground, background);

         if (selectionMode == SelectionMode.UnifiedLength)
         {
            //var padding = unifiedLength - console.CursorLeft;

            var padding = console.CursorLeft;
            if (padding > 0)
               Print(string.Empty.PadRight(padding), foreground, background);
         }

         if (selectionMode == SelectionMode.FullLine)
         {
            var padding = console.WindowWidth - console.CursorLeft - 1;
            Print(string.Empty.PadRight(padding), foreground, background);
         }

         Hint(element, selectionMode, false);
      }

      #endregion

      #region Public Methods and Operators

      public void Hint(ElementInfo element, SelectionMode selectionMode, bool isVisible)
      {
         if (isVisible)
         {
            var disabledHint = element.Hint;

            if (selectionMode == SelectionMode.FullLine)
            {
               console.SetCursorPosition(console.CursorLeft - disabledHint.Length, console.CursorTop);
               Print(disabledHint, DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
            }
            else
            {
               Print(disabledHint, DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
            }

            console.ResetColor();
         }
         else
         {
            var totalWidth = console.WindowWidth - console.CursorLeft - 1;
            Print(string.Empty.PadRight(Math.Max(totalWidth, 0)), DEFAULT_FOREGROUND_COLOR, DEFAULT_BACKGROUND_COLOR);
         }
      }

      public void Selector(ElementInfo element, string selector)
      {
         var background = element.IsSelected ? ConsoleColor.White : ConsoleColor.Black;
         if (element.IsMouseOver && !element.IsSelected)
            background = ConsoleColor.Gray;

         if (element.IsSelected)
         {
            Print(selector, ConsoleColor.Black, background);
         }
         else
         {
            var indentation = string.Empty.PadRight(selector.Length);
            Print(indentation, DEFAULT_FOREGROUND_COLOR, background);
         }
      }

      #endregion

      #region Methods

      private void Print(string text, ConsoleColor foreground, ConsoleColor background)
      {
         if (string.IsNullOrEmpty(text))
            return;

         console.ForegroundColor = foreground;
         console.BackgroundColor = background;

         console.Write(text);
         console.ResetColor();
      }

      private void PrintExpander(ElementInfo element, ConsoleColor itemForeground, ConsoleColor itemBackground)
      {
         var expanderWidth = 1;

         if (!element.IsExpanded.HasValue)
         {
            Print(string.Empty.PadRight(expanderWidth), itemForeground, itemBackground);
         }
         else
         {
            if (element.IsExpanded.Value)
            {
               Print(element.Expander.Expanded, itemForeground, itemBackground);
            }
            else
            {
               Print(element.Expander.Collapsed, itemForeground, itemBackground);
            }
         }
      }

      #endregion
   }
}