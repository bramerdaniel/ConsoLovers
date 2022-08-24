// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

internal class MappingOptions : IMappingOptions
{
   public UnhandledArgumentsBehaviors UnhandledArgumentsBehavior { get; set; } = UnhandledArgumentsBehaviors.LogToConsole;
}