// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKeyInputContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

using ConsoLovers.ConsoleToolkit.InputHandler;

public interface IKeyInputContext
{
   KeyEventArgs KeyEventArgs { get; }

   void Cancel();

   void Cancel(Action cancellationAction);
   
   void Accept();
}