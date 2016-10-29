// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColoredConsole.cs" company="KUKA Roboter GmbH">
//   Copyright (c) KUKA Roboter GmbH 2006 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers
{
   using System;

   /// <summary>Console abstraction that adds some methods for easy colorizing the console</summary>
   /// <seealso cref="ConsoLovers.IConsole"/>
   public interface IColoredConsole : IConsole
   {
      #region Public Methods and Operators

      /// <summary>Clears the console with the given <see cref="ConsoleColor"/>.</summary>
      /// <param name="clearingColor">The <see cref="ConsoleColor"/> to clear the console with.</param>
      void Clear(ConsoleColor clearingColor);

      #endregion
   }
}