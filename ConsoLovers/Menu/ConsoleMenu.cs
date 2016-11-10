﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenu.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.InputHandler;

   public class ConsoleMenu : IConsoleMenuOptions
   {
      #region Constants and Fields

      private readonly Dictionary<string, ElementInfo> indexMap = new Dictionary<string, ElementInfo>();

      private readonly ConsoleMenuItem root = new ConsoleMenuItem("rootItem");

      private bool closed;

      private readonly IDictionary<ConsoleMenuItem, ElementInfo> elements = new Dictionary<ConsoleMenuItem, ElementInfo>();

      private int expanderWidth = 1;

      private ConsoleMenuInputHandler inputHandler;

      private ConsoleMenuItem selectedItem;

      private int unifiedLength;

      private SelectionStrech selectionStrech;

      private string selector = ">> ";

      private bool indexMenuItems = true;

      private int indentSize = 2;

      private ExpanderDescription expander = new ExpanderDescription();

      private bool circularSelection = true;

      private MenuColorTheme theme = new MenuColorTheme();

      private bool clearOnExecution = true;

      private ElementInfo lastMouseOver;

      #endregion

      #region Public Events

      /// <summary>Occurs when an exception is thrown during the execution of an command.</summary>
      public event EventHandler<ExceptionEventArgs> ExecutionError;

      #endregion

      #region IConsoleMenuOptions Members

      /// <summary>Gets or sets a value indicating whether the circular selection is enabled or not.</summary>
      public bool CircularSelection
      {
         get
         {
            return circularSelection;
         }
         set
         {
            if (circularSelection == value)
               return;

            circularSelection = value;
            Invalidate();
         }
      }

      public bool ClearOnExecution
      {
         get
         {
            return clearOnExecution;
         }
         set
         {
            if (clearOnExecution == value)
               return;

            clearOnExecution = value;
            Invalidate();
         }
      }

      public ConsoleKey[] CloseKeys { get; set; } = new ConsoleKey[0];

      public bool ExecuteOnIndexSelection { get; set; }

      public ExpanderDescription Expander
      {
         get
         {
            return expander;
         }
         set
         {
            if (expander == value)
               return;

            expander = value;
            Invalidate();
         }
      }

      /// <summary>Gets or sets the footer that is displayed below the menu.</summary>
      public object Footer { get; set; }

      /// <summary>Gets or sets the header that is displayed.</summary>
      public object Header { get; set; }

      /// <summary>Gets or sets the size of the indent that is used to indent child menu items.</summary>
      public int IndentSize
      {
         get
         {
            return indentSize;
         }
         set
         {
            if (indentSize == value)
               return;

            indentSize = value;
            Invalidate();
         }
      }

      /// <summary>Gets or sets a value indicating whether the <see cref="ConsoleMenuItem"/>s should be displayed and be accessible with an index.</summary>
      public bool IndexMenuItems
      {
         get
         {
            return indexMenuItems;
         }
         set
         {
            if (indexMenuItems == value)
               return;

            indexMenuItems = value;
            Invalidate();
         }
      }

      /// <summary>Gets or sets the selection strech mode that is used for displaying the selection.</summary>
      public SelectionStrech SelectionStrech
      {
         get
         {
            return selectionStrech;
         }
         set
         {
            if (selectionStrech == value)
               return;

            selectionStrech = value;
            Invalidate();
         }
      }

      /// <summary>Gets or sets the selector that is used for displaying the selection.</summary>
      public string Selector
      {
         get
         {
            return selector;
         }
         set
         {
            if (selector == value)
               return;

            selector = value;
            Invalidate();
         }
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the console that is used for printing the menu.</summary>
      public IColoredConsole Console { get; set; } = ColoredConsole.Instance;

      public int Count => root.Items.Count;

      /// <summary>Gets or sets the selected item.</summary>
      public ConsoleMenuItem SelectedItem
      {
         get
         {
            return selectedItem;
         }

         private set
         {
            if (selectedItem == value)
               return;

            RefreshMenuItem(selectedItem, false);
            RefreshMenuItem(value, true);

            selectedItem = value;
         }
      }

      /// <summary>Gets or sets the <see cref="MenuColorTheme"/> the <see cref="ConsoleMenu"/> uses.</summary>
      public MenuColorTheme Theme
      {
         get
         {
            return theme;
         }
         set
         {
            if (theme == value)
               return;

            theme = value;
            Invalidate();
         }
      }

      #endregion

      #region Properties

      #endregion

      #region Public Methods and Operators

      public static IFluentMenu WithHeader(string text)
      {
         return new ConsoleMenuBuilder(text);
      }

      public static IFluentMenu WithoutHeader()
      {
         return new ConsoleMenuBuilder(null);
      }

      public void Add(ConsoleMenuItem item)
      {
         if (item == null)
            throw new ArgumentNullException(nameof(item));

         if (SelectedItem == null)
            selectedItem = item;

         item.Parent = root;
         item.Menu = this;
         root.Items.Add(item);
      }

      public void Clear()
      {
         foreach (var item in root.Items)
         {
            item.Parent = null;
            item.Menu = null;
         }

         root.Items.Clear();
      }

      public void Close()
      {
         inputHandler.Stop();
         closed = true;
      }

      public bool Contains(ConsoleMenuItem item)
      {
         return root.Items.Contains(item);
      }

      public void Insert(int index, ConsoleMenuItem item)
      {
         root.Items.Insert(index, item);
         item.Parent = root;
         item.Menu = this;
      }

      public void Invalidate()
      {
         RefreshMenu();
      }

      /// <summary>Removes the given item from the menu.</summary>
      /// <param name="item">The item to remove.</param>
      /// <returns>True if the item could be removed</returns>
      public bool Remove(ConsoleMenuItem item)
      {
         if (item.Parent == root)
         {
            item.Parent = null;
            item.Menu = null;
         }

         return root.Items.Remove(item);
      }

      public void RemoveAt(int index)
      {
         if (index < 0 || index >= Count)
            return;

         var item = root.Items[index];
         Remove(item);
      }

      public void Show()
      {
         RefreshMenu();

         inputHandler = new ConsoleMenuInputHandler(Console);
         inputHandler.InputChanged += OnInputChanged;
         inputHandler.MouseMoved += OnMouseMoved;
         inputHandler.MouseClicked += OnMouseClicked;
         inputHandler.MouseDoubleClicked += OnMouseDoubleClicked;
         inputHandler.Start();
      }

      private void OnMouseClicked(object sender, MouseEventArgs e)
      {
         var mouseOverElement = GetMouseOverElement(e);
         if (mouseOverElement != null)
         {
            SelectedItem = mouseOverElement.MenuItem;
         }
      }

      private void OnMouseDoubleClicked(object sender, MouseEventArgs e)
      {
         var mouseOverElement = GetMouseOverElement(e);
         if (mouseOverElement != null)
         {
            SelectedItem = mouseOverElement.MenuItem;
            Execute(SelectedItem);
         }
      }

      internal void UpdateMouseOver(ElementInfo value)
      {
         if (lastMouseOver == value)
            return;

         if (lastMouseOver != null)
         {
            lastMouseOver.IsMouseOver = false;
            RefreshMenuItem(lastMouseOver.MenuItem, lastMouseOver.IsSelected);
         }

         if (value != null)
         {
            value.IsMouseOver = true;
            RefreshMenuItem(value.MenuItem, value.IsSelected);
         }

         lastMouseOver = value;
      }

      private void OnMouseMoved(object sender, MouseEventArgs e)
      {
         var mouseOverElement = GetMouseOverElement(e);
         UpdateMouseOver(mouseOverElement);
      }

      private ElementInfo GetMouseOverElement(MouseEventArgs e)
      {
         foreach (var element in elements.Values)
         {
            if (element.Line == e.WindowTop)
            {
               if (selectionStrech == SelectionStrech.FullLine)
                  return element;

               if (selectionStrech == SelectionStrech.UnifiedLength)
               {
                  if (unifiedLength > e.WindowLeft)
                     return element;
               }
               else
               {
                  if (element.Length > e.WindowLeft)
                     return element;
               }
            }
         }

         return null;
      }

      #endregion

      #region Methods

      internal void RefreshMenu()
      {
         Console.Clear(Theme.ConsoleBackground);
         expanderWidth = root.Items.Any(i => i.HasChildren) ? Expander.Length : 0;

         var indexWidth = IndexMenuItems ? 3 + (root.Items.Count < 10 ? 1 : 2) : 0;
         indexMap.Clear();

         UpdateElements();

         PrintHeader();

         if (elements.Count > 0)
         {
            unifiedLength = elements.Values.Max(m => m.Text.Length + m.Indent.Length) + Selector.Length + expanderWidth + indexWidth;
            PrintElements();
         }

         PrintFooter();
      }

      private static string DisabledHint(ConsoleMenuItem menuItem)
      {
         if (menuItem.DisabledHint != null)
            return menuItem.DisabledHint;

         return "[Item is disabled]";
      }

      private bool CanExecute(ConsoleMenuItem menuItem)
      {
         return menuItem.CanExecute();
      }

      private void Collapse(bool selectParentWhenCollapsed)
      {
         if (SelectedItem == null)
            return;

         if (!SelectedItem.IsExpanded)
         {
            if (SelectedItem.Parent == root)
               return;

            if (selectParentWhenCollapsed)
               SelectedItem = SelectedItem.Parent;
         }
         else
         {
            SelectedItem.Collapse();
         }
      }

      private IEnumerable<ElementInfo> CreateElements(IList<ConsoleMenuItem> menuItems, ElementInfo parent, bool useNumbers = true, int indent = 0)
      {
         for (var i = 0; i < menuItems.Count; i++)
         {
            var menuItem = menuItems[i];
            var identifier = useNumbers ? (i + 1).ToString() : ((char)(97 + i)).ToString();
            var elementInfo = new ElementInfo
            {
               Identifier = identifier,
               Path = parent?.Identifier + identifier,
               IndexString = $"[{identifier}] ",
               Text = menuItem.Text,
               MenuItem = menuItem,
               IsSelected = IsSelected(menuItem),
               Disabled = !CanExecute(menuItem),
               Hint = DisabledHint(menuItem),
               Indent = string.Empty.PadRight(indent * IndentSize),
               IsExpanded = menuItem.HasChildren ? (bool?)menuItem.IsExpanded : null
            };

            indexMap.Add(parent == null ? identifier : parent.Path + identifier, elementInfo);
            yield return elementInfo;

            if (menuItem.CanExpand() && menuItem.IsExpanded)
            {
               foreach (var nestedElement in CreateElements(menuItem.Items, elementInfo, !useNumbers, indent + 1))
                  yield return nestedElement;
            }
         }
      }

      private void Execute(ConsoleMenuItem menuItem)
      {
         if (!menuItem.CanExecute())
         {
            RefreshMenuItem(menuItem, true, true);
            return;
         }

         var consoleWasCleared = ClearOnExecution;
         if (consoleWasCleared)
            Console.Clear();

         try
         {
            menuItem.Execute();

            if (consoleWasCleared)
               RefreshMenu();
         }
         catch (Exception ex)
         {
            var handler = ExecutionError;
            if (handler == null)
               throw;

            var args = new ExceptionEventArgs(ex);
            handler(this, args);
            if (!args.Handled)
               throw;
         }
      }

      private void Expand(bool moveNextWhenExpanded, bool revursive)
      {
         if (SelectedItem == null)
            return;

         if (SelectedItem.IsExpanded)
         {
            if (moveNextWhenExpanded)
            {
               SelectNext();
            }
         }
         else
         {
            if (SelectedItem.CanExpand())
            {
               SelectedItem.Expand(revursive);
            }
         }
      }

      private bool IsSelected(ConsoleMenuItem menuItem)
      {
         if (menuItem == SelectedItem)
            return true;
         return false;
      }

      private void OnInputChanged(object sender, ConsoleInputEventArgs e)
      {
         if (CloseKeys.Contains(e.KeyInfo.Key))
            Close();

         if (closed)
            return;

         var lastKey = e.KeyInfo;
         if (lastKey.Key == ConsoleKey.Enter)
         {
            if (SelectedItem != null)
            {
               Execute(SelectedItem);
               if (!SelectedItem.ReturnsToMenu)
               {
                  Close();
                  return;
               }
            }

            return;
         }

         UpdateSelection(lastKey, e.Input);
      }

      private void PrintElement(ElementInfo element)
      {
         var foreground = element.IsMouseOver ? Theme.MouseOverForeground : Theme.MenuItem.GetForeground(element.IsSelected, element.Disabled);
         var background = element.IsMouseOver ? Theme.MouseOverBackground : Theme.MenuItem.GetBackground(element.IsSelected, element.Disabled);

         Write(element.Indent, foreground, background);
         WriteExpander(element, foreground, background);

         if (IndexMenuItems)
            Write(element.IndexString, foreground, background);

         Write(element.Text, foreground, background);

         if (SelectionStrech == SelectionStrech.UnifiedLength)
         {
            var padding = unifiedLength - Console.CursorLeft;
            if (padding > 0)
               Write(string.Empty.PadRight(padding), foreground, background);
         }

         if (SelectionStrech == SelectionStrech.FullLine)
         {
            var padding = Console.WindowWidth - Console.CursorLeft - 1;
            Write(string.Empty.PadRight(padding), foreground, background);
         }
      }

      private void PrintElements()
      {
         foreach (var elementInfo in elements.Values)
         {
            elementInfo.Line = Console.CursorTop;

            PrintSelector(elementInfo);
            PrintElement(elementInfo);

            elementInfo.Length = Console.CursorLeft; 

            PrintHint(elementInfo, false);

            Console.WriteLine();
            Console.ResetColor();
         }
      }

      private void PrintFooter()
      {
         var stringFooter = Footer as string;
         if (stringFooter != null)
         {
            Write(stringFooter, Theme.FooterForeground, Theme.FooterBackground);
            Console.WriteLine();
            return;
         }

         var customFooter = Footer as ICustomFooter;
         if (customFooter != null)
         {
            customFooter.PrintFooter();
            return;
         }

         if (Footer != null)
            Console.WriteLine(Footer.ToString());
      }

      private void PrintHeader()
      {
         var stringHeader = Header as string;
         if (stringHeader != null)
         {
            Write(stringHeader, Theme.HeaderForeground, Theme.HeaderBackground);
            Console.WriteLine();
            return;
         }

         var customHeader = Header as ICustomHeader;
         if (customHeader != null)
         {
            customHeader.PrintHeader();
            return;
         }

         if (Header != null)
            Console.WriteLine(Header.ToString());
      }

      private void PrintHint(ElementInfo menuItem, bool show)
      {
         if (show)
         {
            var disabledHint = menuItem.Hint;
            var foreground = Theme.Hint.GetForeground(menuItem.IsSelected, menuItem.Disabled);
            var background = Theme.Hint.GetBackground(menuItem.IsSelected, menuItem.Disabled);

            if (SelectionStrech == SelectionStrech.FullLine)
            {
               Console.SetCursorPosition(Console.CursorLeft - disabledHint.Length, Console.CursorTop);
               Write(disabledHint, foreground, background);
            }
            else
            {
               Write("  ", Theme.MenuItem.Foreground, Theme.ConsoleBackground);
               Write(disabledHint, foreground, background);
            }

            Console.ResetColor();
         }
         else
         {
            var totalWidth = Console.WindowWidth - Console.CursorLeft - 1;
            Write(string.Empty.PadRight(Math.Max(totalWidth, 0)), Theme.ConsoleBackground, Theme.ConsoleBackground);
         }
      }

      private void PrintSelector(ElementInfo element)
      {
         var foreground = element.IsMouseOver ? Theme.MouseOverForeground : Theme.Selector.GetForeground(element.IsSelected, element.Disabled);
         var background = element.IsMouseOver ? Theme.MouseOverBackground :Theme.Selector.GetBackground(element.IsSelected, element.Disabled);

         if (element.IsSelected)
         {
            Write(Selector, foreground, background);
         }
         else
         {
            Write(string.Empty.PadRight(Selector.Length), foreground, background);
         }
      }

      private void SelectNext()
      {
         if (SelectedItem == null)
         {
            SelectedItem = root.Items.FirstOrDefault();
         }
         else
         {
            var nextItem = SelectedItem.Next();
            if (nextItem != null)
            {
               SelectedItem = nextItem;
            }
            else
            {
               if (CircularSelection)
                  SelectedItem = root.Items.FirstOrDefault();
            }
         }
      }

      private void SelectPrevious()
      {
         if (SelectedItem == null)
         {
            SelectedItem = root.Items.LastOrDefault();
         }
         else
         {
            var previousItem = SelectedItem.Previous();
            if (previousItem != null)
            {
               if (previousItem == root)
               {
                  if (CircularSelection)
                     SelectedItem = root.Items.LastOrDefault();
               }
               else
               {
                  SelectedItem = previousItem;
               }
            }
         }
      }

      private void RefreshMenuItem(ConsoleMenuItem itemToUpdate, bool isSelected, bool showHint = false)
      {
         ElementInfo elementToUpdate;
         if (elements.TryGetValue(itemToUpdate, out elementToUpdate))
         {
            elementToUpdate.IsSelected = isSelected;
            Console.CursorTop = elementToUpdate.Line;
            Console.CursorLeft = 0;

            PrintSelector(elementToUpdate);
            PrintElement(elementToUpdate);
            PrintHint(elementToUpdate, showHint);
         }
      }

      private void UpdateElements()
      {
         elements.Clear();
         foreach (var element in CreateElements(root.Items, null))
            elements[element.MenuItem] = element;
      }

      private void UpdateSelection(ConsoleKeyInfo lastKey, string input)
      {
         switch (lastKey.Key)
         {
            case ConsoleKey.F5:
               RefreshMenu();
               return;
            case ConsoleKey.DownArrow:
               SelectNext();
               return;
            case ConsoleKey.UpArrow:
               SelectPrevious();
               return;
            case ConsoleKey.RightArrow:
               Expand(true, false);
               return;
            case ConsoleKey.Add:
               Expand(false, false);
               break;
            case ConsoleKey.Multiply:
               Expand(false, true);
               break;
            case ConsoleKey.LeftArrow:
               Collapse(true);
               return;
            case ConsoleKey.Subtract:
               Collapse(false);
               break;
            case ConsoleKey.Backspace:
               if (SelectedItem == null || SelectedItem.Parent == root)
                  return;

               SelectedItem = SelectedItem.Parent;
               break;
            case ConsoleKey.Home:
               SelectedItem = root.Items.FirstOrDefault();
               return;
            case ConsoleKey.End:
               SelectedItem = elements.Values.LastOrDefault()?.MenuItem;
               return;
         }

         if (!IndexMenuItems)
            return;

         ElementInfo element;
         if (indexMap.TryGetValue(input, out element))
         {
            SelectedItem = element.MenuItem;

            if (ExecuteOnIndexSelection && SelectedItem != null)
            {
               if (SelectedItem.IsExpanded)
                  SelectedItem.Collapse();

               Execute(SelectedItem);
            }
         }
      }

      private void Write(string text, Color foreground, Color background)
      {
         if (string.IsNullOrEmpty(text))
            return;

         Console.ForegroundColor = foreground;
         Console.BackgroundColor = background;
         Console.Write(text);
         Console.ResetColor();
      }

      private void WriteExpander(ElementInfo elementInfo, Color itemForeground, Color itemBackground)
      {
         if (!elementInfo.IsExpanded.HasValue)
         {
            Write(string.Empty.PadRight(expanderWidth), itemForeground, itemBackground);
         }
         else
         {
            itemForeground = Theme.Expander.GetForeground(elementInfo.IsSelected, elementInfo.Disabled);
            itemBackground = Theme.Expander.GetBackground(elementInfo.IsSelected, elementInfo.Disabled);

            if (elementInfo.IsExpanded.Value)
            {
               Write(Expander.Expanded, itemForeground, itemBackground);
            }
            else
            {
               Write(Expander.Collapsed, itemForeground, itemBackground);
            }
         }
      }

      #endregion
   }
}