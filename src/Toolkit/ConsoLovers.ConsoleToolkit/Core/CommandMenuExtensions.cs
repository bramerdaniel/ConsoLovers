// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.Builders;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   [UsedImplicitly]
   public static class CommandMenuExtensions
   {
      #region Public Methods and Operators

      public static IApplicationBuilder<T> UseMenuWithoutArguments<T>(this IApplicationBuilder<T> bootstrapper,
         Action<ICommandMenuOptions> configureOptions)
         where T : class
      {
         bootstrapper.AddService(s => s.AddSingleton<ICommandMenuManager, CommandMenuManager>());
         bootstrapper.AddService(s => s.AddSingleton<IApplicationLogic, ShowMenuApplicationLogic>());
         bootstrapper.AddService(s => s.AddSingleton<IMenuArgumentManager, MenuArgumentManager>());

         if (configureOptions != null)
            ConfigureMenu(bootstrapper, configureOptions);

         return bootstrapper;
      }

      [UsedImplicitly]
      public static IApplicationBuilder<T> UseMenuWithoutArguments<T>(this IApplicationBuilder<T> bootstrapper)
         where T : class
      {
         return bootstrapper.UseMenuWithoutArguments(null);
      }

      #endregion

      #region Methods

      private static void ConfigureMenu<T>(IApplicationBuilder<T> bootstrapper, Action<ICommandMenuOptions> configureOptions)
         where T : class
      {
         if (bootstrapper is IServiceConfigurationHandler handler)
         {
            handler.ConfigureRequiredService<ICommandMenuManager>(menuManager =>
            {
               var consoleMenuOptions = new CommandMenuOptions();
               configureOptions(consoleMenuOptions);
               menuManager.UseOptions(consoleMenuOptions);
            });
         }
      }

      #endregion
   }
}