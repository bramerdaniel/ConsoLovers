// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandMenuManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   /// <summary>Service that can show a consolovers menu from the defined command structure </summary>
   public interface ICommandMenuManager
   {
      #region Public Methods and Operators

      void Show<T>();

      void UseOptions(ICommandMenuOptions options);

      #endregion
   }
}