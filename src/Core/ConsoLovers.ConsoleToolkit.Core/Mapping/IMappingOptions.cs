// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMappingOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

public interface IMappingOptions
{
   UnhandledArgumentsBehaviors UnhandledArgumentsBehavior { get; set; }
}