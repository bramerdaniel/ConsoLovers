// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SharedArgsInitializer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands;

using ConsoLovers.ConsoleToolkit.Core;

using MenusAndCommands.Commands;

using Microsoft.Extensions.DependencyInjection;

internal class SharedArgsInitializer : IMenuInitializer
{
   public void Initialize(IMenuInitializationContext context)
   {
      var argumentManager = context.ServiceProvider.GetRequiredService<IMenuArgumentManager>();
      var sharedArgs = argumentManager.GetOrCreate<SharedArgs>();
      
      sharedArgs.UserName = "Admin";
      sharedArgs.Password = "Admin";
   }
}