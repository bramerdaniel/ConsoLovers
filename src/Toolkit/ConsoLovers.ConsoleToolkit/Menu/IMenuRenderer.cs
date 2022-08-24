// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using ConsoLovers.ConsoleToolkit.Contracts;

   internal interface IMenuRenderer
   {
      #region Public Methods and Operators

      void Element(ElementInfo element, string selector, SelectionMode selectionMode);

      void Footer(object footer);

      void Header(object header);

      #endregion
   }
}