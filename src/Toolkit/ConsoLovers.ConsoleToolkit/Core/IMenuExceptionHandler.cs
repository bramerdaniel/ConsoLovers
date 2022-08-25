// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuExceptionHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   public interface IMenuExceptionHandler
   {
      #region Public Methods and Operators

      /// <summary>Called when an <see cref="Exception"/> is thrown when a menu command was executed.</summary>
      /// <param name="exception">The exception.</param>
      /// <returns>True if the exception was handled, otherwise false</returns>
      bool Handle(Exception exception);

      #endregion
   }
}