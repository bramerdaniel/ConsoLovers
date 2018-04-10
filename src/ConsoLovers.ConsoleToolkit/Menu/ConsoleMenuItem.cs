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
   public sealed class ConsoleMenuItem : PrintableItem
   {
      #region Constants and Fields

      private readonly Func<bool> canExecute;

      private readonly bool clearItemsOnCollapse;

      private readonly Action<ConsoleMenuItem> execute;

      private readonly Func<IEnumerable<ConsoleMenuItem>> loadChildren;

      private bool isExpanded;

      internal List<PrintableItem> items;

      private bool loadingChildren;

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
         this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
      }

      public ConsoleMenuItem(string text, Action<ConsoleMenuItem> execute, Func<bool> canExecute)
         : this(text)
      {
         this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
         this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
      }

      public ConsoleMenuItem(string text)
      {
         Text = text ?? throw new ArgumentNullException(nameof(text));
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
         private set => isExpanded = value;
      }

      public List<PrintableItem> Items => items ?? (items = new List<PrintableItem>());

      public bool ReturnsToMenu { get; set; } = true;

      public string Text
      {
         get => loadingChildren ? text + " [loading children]" : text;

         set => text = value;
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
         return Parent != null && ((ConsoleMenuItem)Parent).items.Remove(this);
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

         var currentIndex = ((ConsoleMenuItem)Parent).items.IndexOf(this);
         if (currentIndex == 0)
            return ((ConsoleMenuItem)Parent);

         var previousIndex = currentIndex - 1;

         while (previousIndex >= 0)
         {
            if (((ConsoleMenuItem)Parent).items[previousIndex] is ConsoleMenuItem previous)
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

         return ((ConsoleMenuItem)Parent);
      }

      private ConsoleMenuItem Next(bool firstChildWhenExpanded)
      {
         if (firstChildWhenExpanded && IsExpanded && HasChildren)
            return items?.OfType<ConsoleMenuItem>().FirstOrDefault();

         if (((ConsoleMenuItem)Parent)?.items == null)
            return null;

         var currentIndex = ((ConsoleMenuItem)Parent).items.IndexOf(this);
         var nextIndex = currentIndex + 1;

         while (nextIndex < ((ConsoleMenuItem)Parent).items.Count)
         {
            var item = ((ConsoleMenuItem)Parent).items[nextIndex] as ConsoleMenuItem;
            if (item != null)
               return item;

            nextIndex++;
         }

         return ((ConsoleMenuItem)Parent).Next(false);
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
}