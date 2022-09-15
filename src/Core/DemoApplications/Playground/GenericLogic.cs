// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public class GenericLogic : IApplicationLogic<ApplicationArgs>
{
   public Task ExecuteAsync(ApplicationArgs arguments, CancellationToken cancellationToken)
   {
      return Task.CompletedTask;
   }
}