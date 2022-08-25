// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuFooter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Contracts
{
   using ConsoLovers.ConsoleToolkit.Core;

   /// <summary>Interface for customizing the menus footer</summary>
   public interface IMenuFooter
   {
      #region Public Methods and Operators

      /// <summary>Prints the footer of the menu.</summary>
      void PrintFooter(IConsole console);

      #endregion
   }
}