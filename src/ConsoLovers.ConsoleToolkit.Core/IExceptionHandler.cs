// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExeptionHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   /// <summary>Interface that can be implemented by an <see cref="IApplication"/> to handle unhandle <see cref="Exception"/>s</summary>
   public interface IExceptionHandler
   {
      #region Public Methods and Operators

      /// <summary>Handler method the should check if it is an known error e.g. missing command line argument</summary>
      /// <param name="exception">The exception that occured.</param>
      /// <returns>True if the exception was handled, otherwise false</returns>
      bool HandleException(Exception exception);

      #endregion
   }
}