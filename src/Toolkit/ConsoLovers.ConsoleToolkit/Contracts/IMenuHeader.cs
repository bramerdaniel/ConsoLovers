// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuHeader.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Contracts
{
   using ConsoLovers.ConsoleToolkit.Core;

   /// <summary>Interface for customizing the menus header.</summary>
   public interface IMenuHeader
   {
      #region Public Methods and Operators

      /// <summary>Prints the header of the menu.</summary>
      void PrintHeader(IConsole console);

      #endregion
   }
}