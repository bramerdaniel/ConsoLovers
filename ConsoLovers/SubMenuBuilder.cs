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

   public class SubMenuBuilder : ISubMenuBuilder
   {
      #region Constants and Fields

      private readonly List<ConsoleMenuItem> menuItems = new List<ConsoleMenuItem>(5);

      private readonly IMenuBuilder parent;

      #endregion

      #region Constructors and Destructors

      public SubMenuBuilder(IMenuBuilder parent, string text)
      {
         if (parent == null)
            throw new ArgumentNullException(nameof(parent));
         if (text == null)
            throw new ArgumentNullException(nameof(text));

         this.parent = parent;
         Text = text;
      }

      #endregion

      #region IAddMenuItem Members

      public void AddItem(ConsoleMenuItem item)
      {
         menuItems.Add(item);
      }

      #endregion

      #region IMenuBuilder Members

      public void Show()
      {
         Done();
         parent.Show();
      }

      #endregion

      #region ISubMenuBuilder Members

      /// <summary>Finishes the creation of the menu and returns to the parent.</summary>
      /// <returns>The parent menu builder</returns>
      public IMenuBuilder Done()
      {
         var subMenu = new ConsoleMenuItem(Text, menuItems.ToArray());
         parent.AddItem(subMenu);

         return parent;
      }

      public T Done<T>() where T : class, IMenuBuilder
      {
         return Done() as T;
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

      public ISubMenuBuilder WithSubMenu(string text)
      {
         return new SubMenuBuilder(this, text);
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the text of the menu item.</summary>
      public string Text { get; }

      #endregion
   }
}