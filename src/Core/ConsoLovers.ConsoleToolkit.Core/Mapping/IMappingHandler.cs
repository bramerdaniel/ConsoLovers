// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMappingHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public interface IMappingHandler
{
   bool TryMap(CommandLineArgument argument);
}