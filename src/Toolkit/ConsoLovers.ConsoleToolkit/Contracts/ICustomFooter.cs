// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomFooter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Contracts
{
   /// <summary>Interface for customizing the menus footer</summary>
   public interface ICustomFooter
   {
      #region Public Methods and Operators

      /// <summary>Prints the footer of the menu.</summary>
      void PrintFooter();

      #endregion
   }
}