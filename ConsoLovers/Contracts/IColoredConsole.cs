// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColoredConsole.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Contracts
{
   using System;
   using System.Drawing;

   /// <summary>ColoredConsole abstraction that adds some methods for easy colorizing the console</summary>
   /// <seealso cref="IConsole"/>
   public interface IColoredConsole : IConsole
   {
      #region Public Methods and Operators

      /// <summary>Clears the console with the given <see cref="ConsoleColor"/>.</summary>
      /// <param name="clearingColor">The <see cref="ConsoleColor"/> to clear the console with.</param>
      void Clear(Color clearingColor);

      #endregion
   }
}