// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputMask.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   public interface IInputMask
   {
      #region Public Methods and Operators

      /// <summary>Reads the following input with the default mask of *.</summary>
      /// <returns>The input as string</returns>
      string Read();

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <returns>The input as string</returns>
      string ReadLine();

      #endregion
   }
}