// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKeyInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using ConsoLovers.ConsoleToolkit.InputHandler;

public interface IKeyInputHandler
{
   void HandleKeyInput(IKeyInputContext context);
}