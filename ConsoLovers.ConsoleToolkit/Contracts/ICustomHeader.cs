// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomHeader.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Contracts
{
   /// <summary>Interface for customizing the menus header.</summary>
   public interface ICustomHeader
   {
      #region Public Methods and Operators

      /// <summary>Prints the header of the menu.</summary>
      void PrintHeader();

      #endregion
   }
}