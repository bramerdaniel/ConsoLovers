// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.Builders;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.DependencyInjection.Extensions;

   [UsedImplicitly]
   public static class CommandMenuExtensions
   {
      #region Public Methods and Operators

      public static IApplicationBuilder<T> UseMenuWithoutArguments<T>(this IApplicationBuilder<T> bootstrapper,
         Action<ICommandMenuOptions> configureOptions)
         where T : class
      {
         bootstrapper.AddService(s => s.AddSingleton<IMenuCommandManager, MenuCommandManager>());
         bootstrapper.AddService(s => s.AddSingleton<IApplicationLogic, ShowMenuApplicationLogic>());
         bootstrapper.AddService(s => s.AddSingleton<IMenuArgumentManager, MenuArgumentManager>());
         bootstrapper.AddService(s => s.AddSingleton<IMenuExceptionHandler, MenuExceptionHandler>());
         bootstrapper.AddService(s => s.TryAddSingleton<IInputReader, ConsoleInputReader>());

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
            handler.ConfigureRequiredService<IMenuCommandManager>(menuManager =>
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