// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomizedFooter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public interface ICustomizedFooter
{
   #region Public Methods and Operators

   /// <summary>Writes the custom footer to the console.</summary>
   void WriteFooter(IConsole console);

   #endregion
}