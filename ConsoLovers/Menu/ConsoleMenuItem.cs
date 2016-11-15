// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuItem.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;

   [DebuggerDisplay("{Text}")]
   public class ConsoleMenuItem : PrintableItem
   {
      #region Constants and Fields

      private readonly Func<bool> canExecute;

      private readonly bool clearItemsOnCollapse;

      private readonly Action<ConsoleMenuItem> execute;

      private readonly Func<IEnumerable<ConsoleMenuItem>> loadChildren;

      private bool isExpanded;

      private List<PrintableItem> items;

      private bool loadingChildren = false;

      private string text;

      #endregion

      #region Constructors and Destructors

      public ConsoleMenuItem(string text, Func<IEnumerable<ConsoleMenuItem>> loadChildren, bool clearItemsOnCollapse)
         : this(text)
      {
         this.loadChildren = loadChildren;
         this.clearItemsOnCollapse = clearItemsOnCollapse;
         execute = SwapExpand;
      }

      public ConsoleMenuItem(string text, params PrintableItem[] children)
         : this(text)
      {
         items = new List<PrintableItem>(children.Length);
         foreach (var child in children)
         {
            child.Parent = this;
            items.Add(child);
         }

         execute = SwapExpand;
      }

      public ConsoleMenuItem(string text, Action<ConsoleMenuItem> execute)
         : this(text)
      {
         if (execute == null)
            throw new ArgumentNullException(nameof(execute));
         this.execute = execute;
      }

      public ConsoleMenuItem(string text, Action<ConsoleMenuItem> execute, Func<bool> canExecute)
         : this(text)
      {
         if (execute == null)
            throw new ArgumentNullException(nameof(execute));
         if (canExecute == null)
            throw new ArgumentNullException(nameof(canExecute));

         this.execute = execute;
         this.canExecute = canExecute;
      }

      public ConsoleMenuItem(string text)
      {
         if (text == null)
            throw new ArgumentNullException(nameof(text));
         Text = text;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the disabled hint that is displayed when the disabled item is executed.</summary>
      public string DisabledHint { get; set; }

      public bool HasChildren => (items != null && items.Count > 0) || loadChildren != null;

      public bool IsExpanded
      {
         get
         {
            if (items == null || items.Count <= 0)
               return false;

            return isExpanded;
         }
         private set
         {
            isExpanded = value;
         }
      }

      public List<PrintableItem> Items
      {
         get
         {
            return items ?? (items = new List<PrintableItem>());
         }

         private set
         {
            items = value;
         }
      }

      public bool ReturnsToMenu { get; set; } = true;

      public virtual string Text
      {
         get
         {
            return loadingChildren ? text + " [loading children]" : text;
         }

         set
         {
            text = value;
         }
      }

      #endregion

      #region Public Methods and Operators

      public bool CanCollapse()
      {
         return items != null;
      }

      public bool CanExecute()
      {
         return execute != null && (canExecute == null || canExecute());
      }

      public bool CanExpand() => items != null || loadChildren != null;

      public bool Collapse()
      {
         if (IsExpanded)
         {
            IsExpanded = false;
            if (clearItemsOnCollapse)
               items = null;

            Menu.Invalidate();
            return true;
         }

         return false;
      }

      public void Execute()
      {
         execute(this);
      }

      public void Expand(bool recursive)
      {
         ExpandInternal(recursive);
      }

      private void ExpandInternal(bool recursive, bool invalidate = true)
      {
         if (items == null && loadChildren != null)
         {
            try
            {
               loadingChildren = true;
               items = new List<PrintableItem>(5);
               foreach (var child in loadChildren())
               {
                  child.Parent = this;
                  child.Menu = Menu;
                  items.Add(child);
                  IsExpanded = true;

                  Menu.Invalidate();
               }
            }
            finally
            {
               loadingChildren = false;
               Menu.Invalidate();
            }
         }

         IsExpanded = true;

         if (recursive)
         {
            foreach (var item in Items.OfType<ConsoleMenuItem>())
            {
               if (item.HasChildren && item.CanExpand())
                  item.ExpandInternal(true, false);
            }
         }

         if (invalidate)
            Menu.Invalidate();
      }

      /// <summary>Removes this item from its menu.</summary>
      /// <returns>True if the item could be removed</returns>
      public bool Remove()
      {
         return Parent != null && Parent.items.Remove(this);
      }

      #endregion

      #region Methods

      internal ConsoleMenuItem Next()
      {
         return Next(true);
      }

      internal ConsoleMenuItem Previous()
      {
         if (Parent == null)
            return null;

         var currentIndex = Parent.items.IndexOf(this);
         if (currentIndex == 0)
            return Parent;

         var previousIndex = currentIndex - 1;

         while (previousIndex >= 0)
         {
            var previous = Parent.items[previousIndex] as ConsoleMenuItem;
            if (previous != null)
            {
               if (previous.IsExpanded)
               {
                  var item = previous.items.OfType<ConsoleMenuItem>().Last();
                  while (item.IsExpanded)
                  {
                     item = item.Items.OfType<ConsoleMenuItem>().Last();
                  }

                  return item;
               }

               return previous;
            }

            previousIndex--;
         }

         return Parent;
      }

      private ConsoleMenuItem Next(bool firstChildWhenExpanded)
      {
         if (firstChildWhenExpanded && IsExpanded && HasChildren)
            return items?.OfType<ConsoleMenuItem>().FirstOrDefault();

         if (Parent == null || Parent.items == null)
            return null;

         var currentIndex = Parent.items.IndexOf(this);
         var nextIndex = currentIndex + 1;

         while (nextIndex < Parent.items.Count)
         {
            var item = Parent.items[nextIndex] as ConsoleMenuItem;
            if (item != null)
               return item;

            nextIndex++;
         }

         return Parent.Next(false);
      }

      private void SwapExpand(ConsoleMenuItem sender)
      {
         if (Parent == null)
            return;

         if (IsExpanded)
         {
            Collapse();
         }
         else
         {
            Expand(false);
         }
      }

      #endregion
   }

   public class PrintableItem
   {
      private ConsoleMenu menu;

      /// <summary>Gets the <see cref="ConsoleMenu"/> the item is part of.</summary>
      public ConsoleMenu Menu
      {
         get
         {
            return menu ?? Parent?.Menu;
         }

         internal set
         {
            menu = value;
         }
      }

      /// <summary>Gets the menu the item is part of.</summary>
      public ConsoleMenuItem Parent { get; internal set; }
   }

   public class ConsoleMenuSeperator : PrintableItem
   {
   }
}