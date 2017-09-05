// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColoredConsoleMenuItem.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;

   [DebuggerDisplay("{" + nameof(Text) + "}")]
   public class ColoredConsoleMenuItem : PrintableItem
   {
      #region Constants and Fields

      private readonly Func<bool> canExecute;

      private readonly bool clearItemsOnCollapse;

      private readonly Action<ColoredConsoleMenuItem> execute;

      private readonly Func<IEnumerable<ColoredConsoleMenuItem>> loadChildren;

      private bool isExpanded;

      private List<PrintableItem> items;

      private bool loadingChildren;

      private string text;

      #endregion

      #region Constructors and Destructors

      public ColoredConsoleMenuItem(string text, Func<IEnumerable<ColoredConsoleMenuItem>> loadChildren, bool clearItemsOnCollapse)
         : this(text)
      {
         this.loadChildren = loadChildren;
         this.clearItemsOnCollapse = clearItemsOnCollapse;
         execute = SwapExpand;
      }

      public ColoredConsoleMenuItem(string text, params PrintableItem[] children)
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

      public ColoredConsoleMenuItem(string text, Action<ColoredConsoleMenuItem> execute)
         : this(text)
      {
         this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
      }

      public ColoredConsoleMenuItem(string text, Action<ColoredConsoleMenuItem> execute, Func<bool> canExecute)
         : this(text)
      {
         this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
         this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
      }

      public ColoredConsoleMenuItem(string text)
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

      public List<PrintableItem> Items
      {
         get => items ?? (items = new List<PrintableItem>());
      }

      public bool ReturnsToMenu { get; set; } = true;

      public virtual string Text
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

      /// <summary>Removes this item from its menu.</summary>
      /// <returns>True if the item could be removed</returns>
      public bool Remove()
      {
         return Parent != null && ((ConsoleMenuItem)Parent).items.Remove(this);
      }

      #endregion

      #region Methods

      internal ColoredConsoleMenuItem Next()
      {
         return Next(true);
      }

      internal ColoredConsoleMenuItem Previous()
      {
         if (Parent == null)
            return null;

         var currentIndex = ((ColoredConsoleMenuItem)Parent).items.IndexOf(this);
         if (currentIndex == 0)
            return ((ColoredConsoleMenuItem)Parent);

         var previousIndex = currentIndex - 1;

         while (previousIndex >= 0)
         {
            var previous = ((ColoredConsoleMenuItem)Parent).items[previousIndex] as ColoredConsoleMenuItem;
            if (previous != null)
            {
               if (previous.IsExpanded)
               {
                  var item = previous.items.OfType<ColoredConsoleMenuItem>().Last();
                  while (item.IsExpanded)
                  {
                     item = item.Items.OfType<ColoredConsoleMenuItem>().Last();
                  }

                  return item;
               }

               return previous;
            }

            previousIndex--;
         }

         return ((ColoredConsoleMenuItem)Parent);
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
            foreach (var item in Items.OfType<ColoredConsoleMenuItem>())
            {
               if (item.HasChildren && item.CanExpand())
                  item.ExpandInternal(true, false);
            }
         }

         if (invalidate)
            Menu.Invalidate();
      }

      private ColoredConsoleMenuItem Next(bool firstChildWhenExpanded)
      {
         if (firstChildWhenExpanded && IsExpanded && HasChildren)
            return items?.OfType<ColoredConsoleMenuItem>().FirstOrDefault();

         if (Parent == null || ((ColoredConsoleMenuItem)Parent).items == null)
            return null;

         var currentIndex = ((ColoredConsoleMenuItem)Parent).items.IndexOf(this);
         var nextIndex = currentIndex + 1;

         while (nextIndex < ((ColoredConsoleMenuItem)Parent).items.Count)
         {
            var item = ((ColoredConsoleMenuItem)Parent).items[nextIndex] as ColoredConsoleMenuItem;
            if (item != null)
               return item;

            nextIndex++;
         }

         return ((ColoredConsoleMenuItem)Parent).Next(false);
      }

      private void SwapExpand(ColoredConsoleMenuItem sender)
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