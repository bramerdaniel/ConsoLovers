// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuRenderer.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
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