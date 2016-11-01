// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubMenuBuilder.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.Menu;

   internal class SubMenuBuilder : ISubMenuBuilder, IMenuItemParent
   {
      #region Constants and Fields

      private readonly List<ConsoleMenuItem> menuItems = new List<ConsoleMenuItem>(5);

      private readonly ICanShowMenu parent;

      private readonly string subMenuText;

      #endregion

      #region Constructors and Destructors

      public SubMenuBuilder(ICanShowMenu parent, string text)
      {
         if (parent == null)
            throw new ArgumentNullException(nameof(parent));
         if (text == null)
            throw new ArgumentNullException(nameof(text));

         this.parent = parent;
         subMenuText = text;
      }

      #endregion

      #region ICanShowMenu Members

      public void Show()
      {
         FinishSubMenu();
         parent.Show();
      }

      #endregion

      #region IMenuItemParent Members

      public void AddItem(ConsoleMenuItem item)
      {
         menuItems.Add(item);
      }

      #endregion

      #region ISubMenuBuilder Members

      /// <summary>Finishes the creation of the menu and returns to the parent.</summary>
      /// <returns>The parent menu builder</returns>
      public ICanAddMenuItems FinishSubMenu()
      {
         var subMenu = new ConsoleMenuItem(subMenuText, menuItems.ToArray());

         ((IMenuItemParent)parent).AddItem(subMenu);

         return parent as ICanAddMenuItems;
      }

      public ISubMenuBuilder WithItem(ConsoleMenuItem item)
      {
         menuItems.Add(item);
         return this;
      }

      public ISubMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute)
      {
         menuItems.Add(new ConsoleMenuItem(text, execute));
         return this;
      }

      public ISubMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute, Func<bool> canExecute)
      {
         menuItems.Add(new ConsoleMenuItem(text, execute, canExecute));
         return this;
      }

      public ISubMenuBuilder WithItem(string text, Action execute)
      {
         return WithItem(text, x => execute());
      }

      public ISubMenuBuilder WithItem(string text, Action execute, Func<bool> canExecute)
      {
         return WithItem(text, x => execute(), canExecute);
      }

      public ISubMenuBuilder CreateSubMenu(string text)
      {
         return new SubMenuBuilder(this, text);
      }

      #endregion

      #region Public Methods and Operators

      public T Done<T>() where T : class, ICanShowMenu
      {
         return FinishSubMenu() as T;
      }

      #endregion
   }
}