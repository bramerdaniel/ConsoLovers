// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentSink.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public interface IArgumentSink
{
   bool TakeArgument(CommandLineArgument argument);
}