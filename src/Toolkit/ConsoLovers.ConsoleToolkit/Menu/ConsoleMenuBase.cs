// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuBase.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Runtime.CompilerServices;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.InputHandler;

   using JetBrains.Annotations;

   public abstract class ConsoleMenuBase : IConsoleMenuOptions
   {
      #region Constants and Fields

      private readonly IDictionary<PrintableItem, ElementInfo> elements = new Dictionary<PrintableItem, ElementInfo>();

      private readonly Dictionary<string, ElementInfo> indexMap = new Dictionary<string, ElementInfo>();

      private readonly ConsoleMenuItem root = new ConsoleMenuItem("rootItem");

      private bool attached;

      private bool circularSelection = true;

      private bool clearOnExecution = true;

      private bool closed;

      private ExpanderDescription expander = new ExpanderDescription();

      private int expanderWidth = 1;

      private int indentSize = 2;

      private bool indexMenuItems = true;

      private ConsoleMenuInputHandler inputHandler;

      private ElementInfo lastMouseOver;

      private MouseMode mouseMode = MouseMode.Hover;

      private DefaultMenuPrinter printer;

      private ConsoleMenuItem selectedItem;

      private SelectionMode selectionMode;

      private string selector = ">> ";

      private int unifiedLength;

      #endregion

      #region Constructors and Destructors

      protected ConsoleMenuBase()
      {
         printer = new DefaultMenuPrinter(Console);
      }

      #endregion

      #region Public Events

      /// <summary>Occurs when an exception is thrown during the execution of an command.</summary>
      public event EventHandler<ExceptionEventArgs> ExecutionError;

      public event PropertyChangedEventHandler PropertyChanged;

      #endregion

      #region IConsoleMenuOptions Members

      /// <summary>Gets or sets a value indicating whether the circular selection is enabled or not.</summary>
      public bool CircularSelection
      {
         get => circularSelection;
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
         get => clearOnExecution;
         set
         {
            if (clearOnExecution == value)
               return;

            clearOnExecution = value;
            Invalidate();
         }
      }

      public ConsoleKey[] CloseKeys { get; set; } = Array.Empty<ConsoleKey>();

      public bool ExecuteOnIndexSelection { get; set; }

      public ExpanderDescription Expander
      {
         get => expander;
         set
         {
            if (expander == value)
               return;

            expander = value;
            RefreshMenu();
         }
      }

      /// <summary>Gets or sets the footer that is displayed below the menu.</summary>
      public object Footer { get; set; }

      /// <summary>Gets or sets the header that is displayed.</summary>
      public object Header { get; set; }

      /// <summary>Gets or sets the size of the indent that is used to indent child menu items.</summary>
      public int IndentSize
      {
         get => indentSize;
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
         get => indexMenuItems;
         set
         {
            if (indexMenuItems == value)
               return;

            indexMenuItems = value;
            Invalidate();
         }
      }

      /// <summary>Gets or sets the selection strech mode that is used for displaying the selection.</summary>
      public SelectionMode SelectionMode
      {
         get => selectionMode;
         set
         {
            if (selectionMode == value)
               return;

            selectionMode = value;
            Invalidate();
         }
      }

      /// <summary>Gets or sets the selector that is used for displaying the selection.</summary>
      public string Selector
      {
         get => selector;
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
      public IConsole Console { get; set; } = new ConsoleProxy();

      public int Count => root.Items.Count;

      /// <summary>Gets or sets a value indicating whether the mouse selection is enabled.</summary>
      public MouseMode MouseMode
      {
         get => mouseMode;
         set
         {
            if (value == mouseMode)
               return;

            lastMouseOver = null;
            mouseMode = value;
            AttachMouseEvents(value != MouseMode.Disabled);
            Invalidate();
         }
      }

      /// <summary>Gets or sets the selected item.</summary>
      public ConsoleMenuItem SelectedItem
      {
         get => selectedItem;

         private set
         {
            if (selectedItem == value)
               return;

            RefreshMenuItem(selectedItem, false);
            RefreshMenuItem(value, true);

            selectedItem = value;
         }
      }

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

      public void Add(PrintableItem item)
      {
         if (item == null)
            throw new ArgumentNullException(nameof(item));

         if (SelectedItem == null)
            selectedItem = item as ConsoleMenuItem;

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
         inputHandler.InputChanged -= OnInputChanged;
         AttachMouseEvents(false);

         inputHandler.Stop();
         closed = true;

         OnMenuClosed();
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

      public void Invalidate() => RefreshMenu();

      /// <summary>Removes the given item from the menu.</summary>
      /// <param name="item">The item to remove.</param>
      /// <returns>True if the item could be removed</returns>
      public bool Remove(PrintableItem item)
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
         printer = new DefaultMenuPrinter(Console);

         RefreshMenu();

         inputHandler = new ConsoleMenuInputHandler();
         inputHandler.InputChanged += OnInputChanged;
         AttachMouseEvents(MouseMode != MouseMode.Disabled);
         inputHandler.Start();
      }

      #endregion

      #region Methods

      internal void RefreshMenu()
      {
         Console.Clear(GetConsoleBackground());
         expanderWidth = root.Items.OfType<ConsoleMenuItem>().Any(i => i.HasChildren) ? Expander.Length : 0;

         var indexWidth = IndexMenuItems ? 3 + (root.Items.Count < 10 ? 1 : 2) : 0;
         indexMap.Clear();

         UpdateElements();

         printer.Header(Header);

         if (elements.Any())
         {
            unifiedLength = elements.Values.Max(m => m.Text.Length + m.Indent.Length)
                            + Selector.Length
                            + expanderWidth
                            + indexWidth;

            PrintElements();
         }

         printer.Footer(Footer);
      }

      internal void UpdateMouseOver(ElementInfo value)
      {
         if (lastMouseOver == value)
            return;

         if (lastMouseOver != null)
         {
            lastMouseOver.IsMouseOver = false;
            RefreshMenuItem(lastMouseOver.MenuItem as ConsoleMenuItem, lastMouseOver.IsSelected);
         }

         if (value != null && value.IsSelectable)
         {
            value.IsMouseOver = true;
            RefreshMenuItem(value.MenuItem as ConsoleMenuItem, value.IsSelected);
         }

         lastMouseOver = value;
      }

      protected abstract ConsoleColor GetConsoleBackground();

      protected abstract ConsoleColor GetExpanderBackground(bool isSelected, bool disabled, bool mouseOver);

      protected abstract ConsoleColor GetExpanderForeground(bool isSelected, bool disabled, bool mouseOver);

      protected abstract ConsoleColor GetFooterBackground();

      protected abstract ConsoleColor GetFooterForeground();

      protected abstract ConsoleColor GetHeaderBackground();

      protected abstract ConsoleColor GetHeaderForeground();

      protected abstract ConsoleColor GetHintBackground(bool isSelected, bool disabled);

      protected abstract ConsoleColor GetHintForeground(bool isSelected, bool disabled);

      protected abstract ConsoleColor GetMenuItemBackground(bool isSelected, bool disabled, bool mouseOver, ConsoleColor? elementBackground);

      protected abstract ConsoleColor GetMenuItemForeground(bool isSelected, bool disabled, bool mouseOver, ConsoleColor? elementForeground);

      protected abstract ConsoleColor GetMouseOverBackground();

      //{
      //   return Theme.MouseOverBackground;
      //}

      protected abstract ConsoleColor GetMouseOverForeground();

      protected abstract ConsoleColor GetSelectorBackground(bool isSelected, bool disabled, bool mouseOver);

      protected abstract ConsoleColor GetSelectorForeground(bool isSelected, bool disabled, bool mouseOver);

      protected virtual void OnMenuClosed()
      {
      }

      [NotifyPropertyChangedInvocator]
      protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      private static string DisabledHint(ConsoleMenuItem menuItem)
      {
         if (menuItem.DisabledHint != null)
            return menuItem.DisabledHint;

         return "[Item is disabled]";
      }

      private static string GetText(ConsoleMenuSeparator separator)
      {
         if (separator == null)
            return ConsoleMenuSeparator.DefaultText;
         return separator.GetText();
      }

      private void AttachMouseEvents(bool attach)
      {
         if (attach)
         {
            if (attached)
               return;

            inputHandler.MouseMoved += OnMouseMoved;
            inputHandler.MouseClicked += OnMouseClicked;
            inputHandler.MouseDoubleClicked += OnMouseDoubleClicked;
            attached = true;
         }
         else
         {
            if (!attached)
               return;

            inputHandler.MouseMoved -= OnMouseMoved;
            inputHandler.MouseClicked -= OnMouseClicked;
            inputHandler.MouseDoubleClicked -= OnMouseDoubleClicked;
            attached = false;
         }
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
               SelectedItem = (ConsoleMenuItem) SelectedItem.Parent;
         }
         else
         {
            SelectedItem.Collapse();
         }
      }

      private IEnumerable<ElementInfo> CreateElements(IList<PrintableItem> menuItems, ElementInfo parent, bool useNumbers = true, int indent = 0)
      {
         for (var i = 0; i < menuItems.Count; i++)
         {
            var menuItem = menuItems[i] as ConsoleMenuItem;
            if (menuItem == null)
            {
               var separator = menuItems[i] as ConsoleMenuSeparator;
               var seperatorElement = new ElementInfo
               {
                  Text = GetText(separator),
                  Foreground = separator?.Foreground,
                  Background = separator?.Background,
                  MenuItem = separator,
                  IsSelected = false,
                  Disabled = true,
                  Hint = null,
                  Indent = string.Empty.PadRight(indent * IndentSize),
                  IsExpanded = null,
                  Expander = Expander
               };

               yield return seperatorElement;
               continue;
            }

            var identifier = useNumbers ? (i + 1).ToString() : ((char) (97 + i)).ToString();
            var elementInfo = new ElementInfo
            {
               Identifier = identifier,
               Path = parent?.Identifier + identifier,
               IndexString = $"[{identifier}] ",
               Text = menuItem.Text,
               Foreground = menuItem.Foreground,
               Background = menuItem.Background,
               MenuItem = menuItem,
               IsSelected = IsSelected(menuItem),
               Disabled = !CanExecute(menuItem),
               Hint = DisabledHint(menuItem),
               Indent = string.Empty.PadRight(indent * IndentSize),
               IsExpanded = menuItem.HasChildren ? (bool?) menuItem.IsExpanded : null,
               IsSelectable = true,
               Expander = Expander
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

      private void Expand(bool moveNextWhenExpanded, bool recursive)
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
               SelectedItem.Expand(recursive);
            }
         }
      }

      private ElementInfo GetMouseOverElement(MouseEventArgs e)
      {
         foreach (var element in elements.Values)
         {
            if (element.Line == e.WindowTop)
            {
               if (selectionMode == SelectionMode.FullLine)
                  return element;

               if (selectionMode == SelectionMode.UnifiedLength)
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

      private void OnMouseClicked(object sender, MouseEventArgs e)
      {
         var mouseOverElement = GetMouseOverElement(e);
         if (mouseOverElement != null)
         {
            SelectedItem = mouseOverElement.MenuItem as ConsoleMenuItem;
         }
      }

      private void OnMouseDoubleClicked(object sender, MouseEventArgs e)
      {
         var mouseOverElement = GetMouseOverElement(e);
         if (mouseOverElement != null)
         {
            SelectedItem = mouseOverElement.MenuItem as ConsoleMenuItem;
            if (SelectedItem != null)
               Execute(SelectedItem);
         }
      }

      private void OnMouseMoved(object sender, MouseEventArgs e)
      {
         var mouseOverElement = GetMouseOverElement(e);
         if (MouseMode == MouseMode.Hover)
         {
            UpdateMouseOver(mouseOverElement);
         }
         else if (MouseMode == MouseMode.Select)
         {
            if (mouseOverElement != null)
               SelectedItem = mouseOverElement.MenuItem as ConsoleMenuItem;
         }
      }

      private void PrintElements()
      {
         foreach (var elementInfo in elements.Values)
         {
            elementInfo.Line = Console.CursorTop;

            printer.Element(elementInfo, Selector, SelectionMode);
            elementInfo.Length = Console.CursorLeft;

            Console.WriteLine();
            Console.ResetColor();
         }
      }

      private void RefreshMenuItem(ConsoleMenuItem itemToUpdate, bool isSelected, bool showHint = false)
      {
         if (itemToUpdate == null)
            return;

         if (elements.TryGetValue(itemToUpdate, out var elementToUpdate))
         {
            elementToUpdate.IsSelected = isSelected;
            Console.CursorTop = elementToUpdate.Line;
            Console.CursorLeft = 0;

            printer.Element(elementToUpdate, Selector, SelectionMode);
         }
      }

      private void SelectNext()
      {
         if (SelectedItem == null)
         {
            SelectedItem = root.Items.OfType<ConsoleMenuItem>().FirstOrDefault();
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
                  SelectedItem = root.Items.OfType<ConsoleMenuItem>().FirstOrDefault();
            }
         }
      }

      private void SelectPrevious()
      {
         if (SelectedItem == null)
         {
            SelectedItem = root.Items.OfType<ConsoleMenuItem>().LastOrDefault();
         }
         else
         {
            var previousItem = SelectedItem.Previous();
            if (previousItem != null)
            {
               if (previousItem == root)
               {
                  if (CircularSelection)
                     SelectedItem = root.Items.OfType<ConsoleMenuItem>().LastOrDefault();
               }
               else
               {
                  SelectedItem = previousItem;
               }
            }
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

               SelectedItem = (ConsoleMenuItem) SelectedItem.Parent;
               break;
            case ConsoleKey.Home:
               SelectedItem = root.Items.OfType<ConsoleMenuItem>().FirstOrDefault();
               return;
            case ConsoleKey.End:
               SelectedItem = elements.Values.LastOrDefault()?.MenuItem as ConsoleMenuItem;
               return;
         }

         if (!IndexMenuItems)
            return;

         if (indexMap.TryGetValue(input, out var element))
         {
            SelectedItem = element.MenuItem as ConsoleMenuItem;
            if (ExecuteOnIndexSelection && SelectedItem != null)
            {
               if (SelectedItem.IsExpanded)
                  SelectedItem.Collapse();

               Execute(SelectedItem);
            }
         }
      }

      #endregion
   }
}