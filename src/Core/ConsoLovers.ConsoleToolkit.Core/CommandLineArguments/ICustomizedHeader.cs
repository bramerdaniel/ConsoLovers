﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomHelpHeader.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public interface ICustomizedHeader
{
   #region Public Methods and Operators

   /// <summary>Writes the custom footer to the console.</summary>
   void WriteHeader(IConsole console);

   #endregion
}
public interface ICustomizedFooter
{
   #region Public Methods and Operators

   /// <summary>Writes the custom footer to the console.</summary>
   void WriteFooter(IConsole console);

   #endregion
}