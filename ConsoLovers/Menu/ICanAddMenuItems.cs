// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICanAddMenuItems.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   public interface ICanAddMenuItems : ICanShowMenu
   {
      #region Public Methods and Operators

      ICanAddMenuItems WithItem(ConsoleMenuItem item);

      ICanAddMenuItems WithItem(string text, Action<ConsoleMenuItem> execute);

      ICanAddMenuItems WithItem(string text, Action execute);

      ICanAddMenuItems WithItem(string text, Action<ConsoleMenuItem> execute, Func<bool> canExecute);

      ICanAddMenuItems WithItem(string text, Action execute, Func<bool> canExecute);

      ISubMenuBuilder CreateSubMenu(string text);

      #endregion
   }
}