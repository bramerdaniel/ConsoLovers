// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;
   using System.Runtime.CompilerServices;
   using System.Runtime.InteropServices;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.InputHandler;

   using JetBrains.Annotations;

   [SuppressMessage("ReSharper", "UnusedMember.Global")]
   public abstract class ConsoleMenuBase
   {
      #region Constants and Fields

      private readonly IConsole console;

      private readonly IDictionary<PrintableItem, ElementInfo> elements = new Dictionary<PrintableItem, ElementInfo>();

      private readonly Dictionary<string, ElementInfo> indexMap = new Dictionary<string, ElementInfo>();

      private readonly ConsoleMenuItem root = new ConsoleMenuItem("rootItem");

      private bool attached;


      private bool closed;

      private int expanderWidth = 1;

      private ConsoleMenuInputHandler inputHandler;

      private ElementInfo lastMouseOver;

      private MouseMode mouseMode = MouseMode.Hover;

      private IConsoleMenuOptions options;

      private IMenuRenderer renderer;

      private ConsoleMenuItem selectedItem;

      private int unifiedLength;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleMenuBase"/> class.</summary>
      protected ConsoleMenuBase()
         : this(new ConsoleProxy(), new ConsoleMenuOptions())
      {
      }

      protected ConsoleMenuBase([JetBrains.Annotations.NotNull] IConsole console)
         : this(console, new ConsoleMenuOptions())
      {
      }

      protected ConsoleMenuBase([JetBrains.Annotations.NotNull] IConsoleMenuOptions menuOptions)
         : this(new ConsoleProxy(), menuOptions)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="ConsoleMenuBase"/> class.</summary>
      /// <param name="console">The <see cref="IConsole"/> proxy.</param>
      /// <param name="options">The options.</param>
      /// <exception cref="System.ArgumentNullException">console</exception>
      protected ConsoleMenuBase([JetBrains.Annotations.NotNull] IConsole console, [JetBrains.Annotations.NotNull] IConsoleMenuOptions options)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
         this.options = options ?? throw new ArgumentNullException(nameof(options));
      }

      #endregion

      #region Public Events

      /// <summary>Occurs when an exception is thrown during the execution of an command.</summary>
      public event EventHandler<ExceptionEventArgs> ExecutionError;

      public event PropertyChangedEventHandler PropertyChanged;

      #endregion

      #region Public Properties

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

      /// <summary>Gets or sets the options the menu will use.</summary>
      public IConsoleMenuOptions Options
      {
         get => options;
         set => ExchangeOptions(value);
      }

      private void ExchangeOptions(IConsoleMenuOptions value)
      {
         if (options != null)
            options.PropertyChanged -= OnOptionChanged;

         options = value ?? new ConsoleMenuOptions();
         options.PropertyChanged += OnOptionChanged;
         if (renderer != null)
            renderer.Options = options;

         Invalidate();
      }

      private void OnOptionChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.PropertyName == nameof(Options.Expander))
         {
            RefreshMenu();
         }
         else
         {

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
         renderer = GetMenuRenderer();

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
         console.Clear(GetConsoleBackground());
         expanderWidth = root.Items.OfType<ConsoleMenuItem>().Any(i => i.HasChildren) ? Options.Expander.Length : 0;

         indexMap.Clear();

         UpdateElements();
         RenderMenu();
      }

      private void RenderMenu()
      {
         GetMenuRenderer().Header(Options.Header);

         if (elements.Any())
            RenderElements();

         GetMenuRenderer().Footer(Options.Footer);
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
               SelectedItem = (ConsoleMenuItem)SelectedItem.Parent;
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
               var separatorElement = new ElementInfo
               {
                  Text = GetText(separator),
                  Foreground = separator?.Foreground,
                  Background = separator?.Background,
                  MenuItem = separator,
                  IsSelected = false,
                  Disabled = true,
                  Hint = null,
                  Indent = string.Empty.PadRight(indent * Options.IndentSize),
                  IsExpanded = null,
                  Expander = Options.Expander
               };

               yield return separatorElement;
               continue;
            }

            var identifier = useNumbers ? (i + 1).ToString() : ((char)(97 + i)).ToString();
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
               Indent = string.Empty.PadRight(indent * Options.IndentSize),
               IsExpanded = menuItem.HasChildren ? (bool?)menuItem.IsExpanded : null,
               IsSelectable = true,
               Expander = Options.Expander
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

         var consoleWasCleared = Options.ClearOnExecution;
         if (consoleWasCleared)
            console.Clear();

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

            if (consoleWasCleared)
               RefreshMenu();
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

      private IMenuRenderer GetMenuRenderer()
      {
         if (renderer == null)
         {
            renderer = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
               ? (IMenuRenderer)new DefaultMenuRenderer(console, Options)
               : new LinuxMenuRenderer(console, Options);
         }

         return renderer;
      }

      private ElementInfo GetMouseOverElement(MouseEventArgs e)
      {
         foreach (var element in elements.Values)
         {
            if (element.Line == e.WindowTop)
            {
               if (Options.SelectionMode == SelectionMode.FullLine)
                  return element;

               if (Options.SelectionMode == SelectionMode.UnifiedLength)
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
         if (Options.CloseKeys.Contains(e.KeyInfo.Key))
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

      private void RenderElements()
      {
         var indexWidth = Options.IndexMenuItems ? 3 + (root.Items.Count < 10 ? 1 : 2) : 0;
         unifiedLength = elements.Values.Max(m => m.Text.Length + m.Indent.Length)
                         + Options.Selector.Length
                         + expanderWidth
                         + indexWidth;
         
         foreach (var elementInfo in elements.Values)
         {
            elementInfo.Line = console.CursorTop;

            GetMenuRenderer().Element(elementInfo, unifiedLength);
            elementInfo.Length = console.CursorLeft;

            console.WriteLine();
            console.ResetColor();
         }
      }

      private void RefreshMenuItem(ConsoleMenuItem itemToUpdate, bool isSelected, bool showHint = false)
      {
         if (itemToUpdate == null)
            return;

         var indexWidth = Options.IndexMenuItems ? 3 + (root.Items.Count < 10 ? 1 : 2) : 0;
         unifiedLength = elements.Values.Max(m => m.Text.Length + m.Indent.Length)
                         + Options.Selector.Length
                         + expanderWidth
                         + indexWidth;

         if (elements.TryGetValue(itemToUpdate, out var elementToUpdate))
         {
            elementToUpdate.IsSelected = isSelected;
            console.CursorTop = elementToUpdate.Line;
            console.CursorLeft = 0;

            GetMenuRenderer().Element(elementToUpdate, unifiedLength);
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
               if (Options.CircularSelection)
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
                  if (Options.CircularSelection)
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

               SelectedItem = (ConsoleMenuItem)SelectedItem.Parent;
               break;
            case ConsoleKey.Home:
               SelectedItem = root.Items.OfType<ConsoleMenuItem>().FirstOrDefault();
               return;
            case ConsoleKey.End:
               SelectedItem = elements.Values.LastOrDefault()?.MenuItem as ConsoleMenuItem;
               return;
         }

         if (!Options.IndexMenuItems)
            return;

         if (indexMap.TryGetValue(input, out var element))
         {
            SelectedItem = element.MenuItem as ConsoleMenuItem;
            if (Options.ExecuteOnIndexSelection && SelectedItem != null)
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