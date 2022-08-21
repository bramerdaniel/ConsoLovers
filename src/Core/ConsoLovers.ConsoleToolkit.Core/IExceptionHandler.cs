// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExceptionHandler.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

public interface IExceptionHandler
{
   bool Handle(Exception exception);
}