// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuBuilder.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   public class ConsoleMenuBuilder : IRootMenuBuilder
   {
      #region Constants and Fields

      private ConsoleMenu menu;

      #endregion

      #region Constructors and Destructors

      public ConsoleMenuBuilder()
      {
         menu = new ConsoleMenu();
      }

      #endregion

      #region IMenuBuilder Members

      public void Show()
      {
         Done().Show();
      }

      public IRootMenuBuilder WithItem(ConsoleMenuItem item)
      {
         menu.Add(item);
         return this;
      }

      public IRootMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute)
      {
         menu.Add(new ConsoleMenuItem(text, execute));
         return this;
      }

      public ISubMenuBuilder WithSubMenu(string text)
      {
         return new SubMenuBuilder(this, text);
      }

      #endregion

      #region IRootMenuBuilder Members

      public IRootMenuBuilder WithHeader(string header)
      {
         menu.Header = header;
         return this;
      }

      public IRootMenuBuilder WithFooter(string footer)
      {
         menu.Footer = footer;
         return this;
      }

      public IRootMenuBuilder CloseOn(ConsoleKey key)
      {
         menu.CloseKeys = new[] { key };
         return this;
      }

      #endregion

      #region Public Methods and Operators

      public ConsoleMenu Done()
      {
         return menu;
      }

      #endregion

      public void AddItem(ConsoleMenuItem item)
      {
         WithItem(item);
      }
   }
}