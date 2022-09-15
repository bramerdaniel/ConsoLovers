// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NormalLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public class NormalLogic : IApplicationLogic
{
   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }
}