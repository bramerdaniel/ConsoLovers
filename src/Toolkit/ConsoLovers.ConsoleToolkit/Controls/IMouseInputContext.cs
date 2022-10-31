// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMouseInputContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

public interface IMouseInputContext
{
   void Cancel();

   void Cancel(Action cancellationAction);

   void Accept();
}