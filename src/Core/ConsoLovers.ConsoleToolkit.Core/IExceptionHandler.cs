// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExceptionHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

public interface IExceptionHandler
{
   #region Public Methods and Operators

   /// <summary>Handles the specified exception.</summary>
   /// <param name="exception">The exception.</param>
   /// <returns>True if the exception was handled, otherwise false</returns>
   bool Handle(Exception exception);

   #endregion
}