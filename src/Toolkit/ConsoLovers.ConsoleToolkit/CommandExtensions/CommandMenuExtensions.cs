// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuExtensions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.Builders;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Menu;

   using Microsoft.Extensions.DependencyInjection;

   public static class CommandMenuExtensions
   {
      #region Public Methods and Operators

      public static IApplicationBuilder<T> UseMenuWithoutArguments<T>(this IApplicationBuilder<T> bootstrapper, Action<ICommandMenuOptions> configureOptions)
         where T : class
      {
         bootstrapper.ConfigureServices(s => s.AddSingleton<ICommandMenuManager, CommandMenuManager>());
         bootstrapper.ConfigureServices(s => s.AddSingleton<IApplicationLogic, ShowMenuApplicationLogic>());

         if (configureOptions != null)
            ConfigureMenu(bootstrapper, configureOptions);

         return bootstrapper;
      }

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