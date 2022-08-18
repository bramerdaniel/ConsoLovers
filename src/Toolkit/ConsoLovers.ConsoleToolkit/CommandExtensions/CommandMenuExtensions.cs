// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.BootStrappers;

   using Microsoft.Extensions.DependencyInjection;

   public static class CommandMenuExtensions
   {
      public static IBootstrapper<T> UseMenuFor<T>(this IBootstrapper<T> bootstrapper, Type argumentType)
         where T : class, IApplication
      {
         bootstrapper.ConfigureServices(s => s.AddSingleton<CommandMenuManager>());

         if (bootstrapper is IServiceConfigurationHandler handler)
         {

         }

         return bootstrapper;
      }
   }
}