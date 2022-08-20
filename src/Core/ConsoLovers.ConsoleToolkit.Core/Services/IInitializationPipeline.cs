// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInitializationPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

internal interface IInitializationPipeline
{
   void Execute<T>(IInitializationContext<T> context)
      where T : class;
}