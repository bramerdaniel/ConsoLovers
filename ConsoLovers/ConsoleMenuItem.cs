// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuItem.cs" company="KUKA Roboter GmbH">
//   Copyright (c) KUKA Roboter GmbH 2006 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;

   [DebuggerDisplay("{Text}")]
   public class ConsoleMenuItem 
   {
      private readonly Func<IEnumerable<ConsoleMenuItem>> loadChildren;

      private readonly bool clearItemsOnCollapse;

      #region Constructors and Destructors


      private readonly Action<ConsoleMenuItem> execute;

      private readonly Func<bool> canExecute;

      private bool isExpanded;

      private ConsoleMenu menu;

      private List<ConsoleMenuItem> items;

      private string text;

      private bool loadingChildren = false;

      public ConsoleMenuItem(string text, Func<IEnumerable<ConsoleMenuItem>> loadChildren, bool clearItemsOnCollapse)
         : this(text)
      {
         this.loadChildren = loadChildren;
         this.clearItemsOnCollapse = clearItemsOnCollapse;
         execute = SwapExpand;
      }

      public ConsoleMenuItem(string text, params ConsoleMenuItem[] children)
         : this(text)
      {
         items = new List<ConsoleMenuItem>(children.Length);
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

      public bool CanExpand() => items != null || loadChildren != null;

      /// <summary>Gets or sets the disabled hint that is displayed when the disabled item is executed.</summary>

      public string DisabledHint { get; set; }

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

      public List<ConsoleMenuItem> Items
      {
         get
         {
            return items ?? (items = new List<ConsoleMenuItem>());
         }

         private set
         {
            items = value;
         }
      }

      /// <summary>Gets the menu the item is part of.</summary>
      public ConsoleMenuItem Parent { get; internal set; }

      public bool HasChildren => (items != null && items.Count > 0) || loadChildren != null;

      public bool ReturnsToMenu { get; set; } = true;

      public string Text
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

      #endregion

      #region Public Methods and Operators

      internal ConsoleMenuItem Next()
      {
         return Next(true);
      }

      private ConsoleMenuItem Next(bool firstChildWhenExpanded)
      {
         if (firstChildWhenExpanded && IsExpanded && HasChildren)
            return items?.FirstOrDefault();

         if (Parent == null || Parent.items == null)
            return null;

         var currentIndex = Parent.items.IndexOf(this);
         var nextIndex = currentIndex + 1;

         if (nextIndex >= Parent.items.Count)
            return Parent.Next(false);

         return Parent.items[nextIndex];
      }

      internal ConsoleMenuItem Previous()
      {
         if (Parent == null)
            return null;

         var currentIndex = Parent.items.IndexOf(this);
         if (currentIndex <= 0)
            return Parent;

         var previous = Parent.items[currentIndex - 1];
         if (previous.IsExpanded)
         {
            var item = previous.items.Last();
            while (item.IsExpanded)
            {
               item = item.Items.Last();
            }

            return item;
         }

         return previous;
      }

      public bool CanCollapse()
      {
         return items != null;
      }

      public bool Collapse()
      {
         if (IsExpanded)
         {
            IsExpanded = false;
            if (clearItemsOnCollapse)
               items = null;

            return true;
         }

         return false;
      }

      public void Expand(bool recursive)
      {
         if (items == null && loadChildren != null)
         {
            try
            {
               loadingChildren = true;
               items = new List<ConsoleMenuItem>(5);
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
            foreach (var item in Items)
            {
               if (item.HasChildren && item.CanExpand())
                  item.Expand(true);
            }   
         }
      }

      /// <summary>Removes this item from its menu.</summary>
      /// <returns>True if the item could be removed</returns>
      public bool Remove()
      {
         return Parent != null && Parent.items.Remove(this);
      }

      #endregion

      #region Methods

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

      public bool CanExecute()
      {
         return execute != null && (canExecute == null || canExecute());
      }

      public void Execute()
      {
         execute(this);
      }
   }
}