// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClickable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

internal interface IClickable : IRenderable
{
   void NotifyClicked();
}