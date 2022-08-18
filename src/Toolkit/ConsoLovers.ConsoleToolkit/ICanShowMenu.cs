// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuBuilder.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   public interface ICanShowMenu
   {
      #region Public Methods and Operators

      void Show();

      #endregion
   }

   internal interface IMenuItemParent
   {
      void AddItem(ConsoleMenuItem item);
   }

   public interface ISubMenuBuilder : ICanShowMenu
   {
      #region Public Methods and Operators

      ICanAddMenuItems FinishSubMenu();

      ISubMenuBuilder WithItem(ConsoleMenuItem item);

      ISubMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute);

      ISubMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute, Func<bool> canExecute);

      ISubMenuBuilder WithItem(string text, Action execute);

      ISubMenuBuilder WithItem(string text, Action execute, Func<bool> canExecute);

      ISubMenuBuilder CreateSubMenu(string text);

      #endregion
   }
}