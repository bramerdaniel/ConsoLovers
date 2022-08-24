// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Collections.Generic;

public interface ICommandLineArguments : IList<CommandLineArgument>
{
   #region Public Indexers

   CommandLineArgument this[string name] { get; }

   #endregion

   #region Public Methods and Operators

   bool ContainsName(string name);

   void RemoveFirst(string name);

   bool TryGetValue(string name, out CommandLineArgument argument);

   #endregion
}