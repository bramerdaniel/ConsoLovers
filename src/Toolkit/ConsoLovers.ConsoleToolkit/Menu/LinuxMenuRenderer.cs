// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinuxMenuRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;

   using JetBrains.Annotations;

   internal sealed class LinuxMenuRenderer : IMenuRenderer
   {
      #region Constants and Fields

      private readonly IConsole console;

      private bool IndexMenuItems = true;

      #endregion

      #region Constructors and Destructors

      public LinuxMenuRenderer([NotNull] IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      #endregion

      #region IMenuRenderer Members

      public void Element(ElementInfo element, string selector, SelectionMode selectionMode)
      {
         var selectorText = GetSelector(element, selector);
         var indent = element.Indent;
         var expander = GetExpander(element);

         var index = IndexMenuItems ? element.IndexString : string.Empty;
         var elementText = element.Text;
         var padRight = GetPadRight(selectionMode);

         Print($"{selectorText}{indent}{expander}{index}{elementText}{padRight}");
         //Hint(element, selectionMode, false);
      }

      public void Footer(object footer)
      {
         if (footer is string textFooter)
         {
            Print(textFooter);
            console.WriteLine();
         }
         else if (footer is ICustomFooter customFooter)
         {
            customFooter.PrintFooter();
         }
         else if (footer != null)
         {
            Print(footer.ToString());
         }
         else
         {
            // Ignore empty footer
         }
      }

      public void Header(object header)
      {
         if (header is string textHeader)
         {
            Print(textHeader);
            console.WriteLine();
         }
         else if (header is ICustomHeader customHeader)
         {
            customHeader.PrintHeader();
         }
         else if (header != null)
         {
            Print(header.ToString());
         }
         else
         {
            // Ignore empty header
         }
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
               System.Console.SetCursorPosition(System.Console.CursorLeft - disabledHint.Length, System.Console.CursorTop);
               Print(disabledHint);
            }
            else
            {
               Print(disabledHint);
            }

            console.ResetColor();
         }
         else
         {
            var totalWidth = System.Console.WindowWidth - System.Console.CursorLeft - 1;
            Print(string.Empty.PadRight(Math.Max(totalWidth, 0)));
         }
      }

      #endregion

      #region Methods

      private string GetExpander(ElementInfo element)
      {
         const int expanderWidth = 1;

         if (!element.IsExpanded.HasValue)
            return string.Empty.PadRight(expanderWidth);

         return element.IsExpanded.Value ? element.Expander.Expanded : element.Expander.Collapsed;
      }

      private string GetPadRight(SelectionMode selectionMode)
      {
         if (selectionMode == SelectionMode.UnifiedLength)
         {
            //var padding = unifiedLength - Console.CursorLeft;

            var padding = System.Console.CursorLeft;
            if (padding > 0)
               return string.Empty.PadRight(padding);
         }

         if (selectionMode == SelectionMode.FullLine)
         {
            var padding = System.Console.WindowWidth - System.Console.CursorLeft - 1;
            return string.Empty.PadRight(padding);
         }

         return string.Empty;
      }

      private string GetSelector(ElementInfo element, string selector)
      {
         if (element.IsSelected)
            return selector;

         return string.Empty.PadRight(selector.Length);
      }

      private void Print(string text)
      {
         if (string.IsNullOrEmpty(text))
            return;

         console.Write(text);
      }

      #endregion
   }
}