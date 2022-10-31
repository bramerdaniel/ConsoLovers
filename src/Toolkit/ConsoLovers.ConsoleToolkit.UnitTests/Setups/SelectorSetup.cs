// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectorSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Controls;

internal class SelectorSetup<T>
{
   #region Constants and Fields

   private List<IRenderable> items = new List<IRenderable>();

   private CSelector<T> selector;

   #endregion

   #region Constructors and Destructors

   public SelectorSetup()
   {
      selector = new CSelector<T>();
   }

   #endregion

   #region Public Methods and Operators

   public CSelector<T> Done()
   {
      return selector;
   }

   public SelectorSetup<T> WithItem(T item)
   {
      selector.Add(item);
      return this;
   }

   public SelectorSetup<T> WithItem(T item, IRenderable template)
   {
      selector.Add(item, template);
      return this;
   }

   public SelectorSetup<T> WithOrientation(Orientation orientation)
   {
      selector.Orientation = orientation;
      return this;
   }

   public SelectorSetup<T> WithSelectedIndex(int index)
   {
      selector.SelectedIndex = index;
      return this;
   }

   public SelectorSetup<T> WithoutSelector()
   {
      return WithSelector(string.Empty);
   }

   public SelectorSetup<T> WithSelector(string value)
   {
      selector.Selector = value;
      return this;
   }

   #endregion
}