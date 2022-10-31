﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMouseInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

internal interface IMouseInputHandler : IRenderable
{
   void HandleMouseInput(IMouseInputContext context);
   
   // void HandleMouseMove(IMouseInputContext context);
}