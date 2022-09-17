// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyCustomLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public class MyCustomLogic : IApplicationLogic
{
   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      throw new InvalidOperationException();
      return Task.CompletedTask;
   }
}