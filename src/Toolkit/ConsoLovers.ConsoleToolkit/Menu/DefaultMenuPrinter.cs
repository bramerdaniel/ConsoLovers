// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultMenuPrinter.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;

   using JetBrains.Annotations;

   internal class DefaultMenuPrinter : IMenuPrinter
   {
      #region Constants and Fields

      private readonly IConsole console;

      private const ConsoleColor ForegroundColor = ConsoleColor.Gray;

      private const ConsoleColor BackgroundColor = ConsoleColor.Black;

      #endregion

      #region IMenuPrinter Members

      public DefaultMenuPrinter([NotNull] IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      public void Selector(ElementInfo element, string selector)
      {
         var background = element.IsSelected ? ConsoleColor.White : ConsoleColor.Black;

         if (element.IsSelected)
         {
            Print(selector, ForegroundColor, background);
         }
         else
         {
            var indentation = string.Empty.PadRight(selector.Length);
            Print(indentation, ForegroundColor, background);
         }

      }

      public void Header(object header)
      {
         if (header is string textHeader)
         {
            Print(textHeader, ForegroundColor, BackgroundColor);
            console.WriteLine();
         }
         else if (header is ICustomHeader customHeader)
         {
            customHeader.PrintHeader();
         }
         else if (header != null)
         {
            Print(header.ToString(), ForegroundColor, BackgroundColor);
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
            Print(textFooter, ForegroundColor, BackgroundColor);
            console.WriteLine();
         }
         else if (footer is ICustomFooter customFooter)
         {
            customFooter.PrintFooter();
         }
         else if (footer != null)
         {
            Print(footer.ToString(), ForegroundColor, BackgroundColor);
         }
         else
         {
            // Ignore empty footer
         }
      }

      public void Hint(ElementInfo element, SelectionMode selectionMode, bool isVisible)
      {
         if (isVisible)
         {
            var disabledHint = element.Hint;

            if (selectionMode == SelectionMode.FullLine)
            {
               Console.SetCursorPosition(Console.CursorLeft - disabledHint.Length, Console.CursorTop);
               Print(disabledHint, ForegroundColor, BackgroundColor);
            }
            else
            {
               Print(disabledHint, ForegroundColor, BackgroundColor);
            }

            console.ResetColor();
         }
         else
         {
            var totalWidth = Console.WindowWidth - Console.CursorLeft - 1;
            Print(string.Empty.PadRight(Math.Max(totalWidth, 0)), ForegroundColor, BackgroundColor);
         }
      }

      public void Element(ElementInfo element, string selector, SelectionMode selectionMode)
      {
         //var ForegroundColor = GetMenuItemForeground(element.IsSelected, element.Disabled, element.IsMouseOver, element.Foreground);
         //var BackgroundColor = GetMenuItemBackground(element.IsSelected, element.Disabled, element.IsMouseOver, element.Background);

         var foreground = Console.ForegroundColor;
         var background = Console.BackgroundColor;

         Selector(element, selector);

         Print(element.Indent, foreground, background);
         PrintExpander(element, foreground, background);

         //if (IndexMenuItems)
         if (true)
            Print(element.IndexString, foreground, background);

         Print(element.Text, foreground, background);

         if (selectionMode == SelectionMode.UnifiedLength)
         {
            //var padding = unifiedLength - Console.CursorLeft;

            var padding = Console.CursorLeft;
            if (padding > 0)
               Print(string.Empty.PadRight(padding), foreground, background);
         }

         if (selectionMode == SelectionMode.FullLine)
         {
            var padding = Console.WindowWidth - Console.CursorLeft - 1;
            Print(string.Empty.PadRight(padding), foreground, background);
         }

         Hint(element, selectionMode, false);
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