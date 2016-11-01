// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuBuilder.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   internal class ConsoleMenuBuilder : IFluentMenu, IMenuItemParent
   {
      #region Constants and Fields

      private readonly ConsoleMenu menu;

      #endregion

      #region Constructors and Destructors

      public ConsoleMenuBuilder(string header)
      {
         menu = new ConsoleMenu { Header = header };
      }

      #endregion

      #region ICanAddMenuItems Members

      public ICanAddMenuItems WithItem(ConsoleMenuItem item)
      {
         menu.Add(item);
         return this;
      }

      public ICanAddMenuItems WithItem(string text, Action<ConsoleMenuItem> execute)
      {
         menu.Add(new ConsoleMenuItem(text, execute));
         return this;
      }

      public ICanAddMenuItems WithItem(string text, Action execute)
      {
         return WithItem(text, x => execute());
      }

      public ICanAddMenuItems WithItem(string text, Action<ConsoleMenuItem> execute, Func<bool> canExecute)
      {
         menu.Add(new ConsoleMenuItem(text, execute, canExecute));
         return this;
      }

      public ICanAddMenuItems WithItem(string text, Action execute, Func<bool> canExecute)
      {
         menu.Add(new ConsoleMenuItem(text, x => execute(), canExecute));
         return this;
      }

      public ISubMenuBuilder CreateSubMenu(string text)
      {
         return new SubMenuBuilder(this, text);
      }

      #endregion

      #region ICanShowMenu Members

      public void Show()
      {
         menu.Show();
      }

      #endregion

      #region IFluentMenu Members

      public IFluentMenu Where(Action<IConsoleMenuOptions> propertySetter)
      {
         propertySetter(menu);
         return this;
      }

      #endregion

      #region IMenuItemParent Members

      void IMenuItemParent.AddItem(ConsoleMenuItem item)
      {
         WithItem(item);
      }

      #endregion
   }
}