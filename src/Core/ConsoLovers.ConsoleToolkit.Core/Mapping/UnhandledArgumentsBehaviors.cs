// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnhandledArgumentsBehaviors.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

[Flags]
public enum UnhandledArgumentsBehaviors
{
   LogToConsole = 0x1,
   CancelExecution = 0x2,
   ThrowException = 0x4,
   UseCustomHandler = 0x8
}