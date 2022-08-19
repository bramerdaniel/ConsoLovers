// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.Builders;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using Microsoft.Extensions.DependencyInjection;

   public static class CommandMenuExtensions
   {
      public static IApplicationBuilder<T> UseMenuWithoutArguments<T>(this IApplicationBuilder<T> bootstrapper)
         where T : class, IApplication
      {
         bootstrapper.ConfigureServices(s => s.AddSingleton<ICommandMenuManager, CommandMenuManager>());
         bootstrapper.ConfigureServices(s => s.AddSingleton<IApplicationLogic, ShowMenuApplicationLogic>());

         if (bootstrapper is IServiceConfigurationHandler handler)
         {

         }

         return bootstrapper;
      }
   }
}