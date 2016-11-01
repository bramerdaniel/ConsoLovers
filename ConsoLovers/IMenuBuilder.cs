// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuBuilder.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IMenuBuilder : IAddMenuItem
   {
      #region Public Methods and Operators

      void Show();

      #endregion
   }

   public interface IAddMenuItem
   {
      void AddItem(ConsoleMenuItem item);
   }

   public interface ISubMenuBuilder : IMenuBuilder
   {
      #region Public Methods and Operators

      IMenuBuilder Done();

      T Done<T>() where T : class, IMenuBuilder;

      ISubMenuBuilder WithItem(ConsoleMenuItem item);

      ISubMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute);

      ISubMenuBuilder WithSubMenu(string text);

      #endregion
   }
}