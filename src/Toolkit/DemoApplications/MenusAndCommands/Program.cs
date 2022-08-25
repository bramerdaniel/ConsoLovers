﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Menu;

   using Microsoft.VisualBasic;

   public static class Program
   {
      #region Methods

      private static async Task Main()
      {
         await ConsoleApplication.WithArguments<AppArguments>()
            .UseMenuWithoutArguments(options =>
            {
               options.Menu.Header = "Hello Menu";
               options.Menu.CloseKeys = new[] { ConsoleKey.Escape };
               options.MenuBehaviour = MenuBuilderBehaviour.ShowAllCommand;

               options.Menu.Expander = new ExpanderDescription { Collapsed = "++", Expanded = "--" };
               options.Menu.CircularSelection = false;
               options.Menu.Selector = "►";
               options.Menu.IndexMenuItems = true;
            })
            .ConfigureCommandLineParser(o => o.CaseSensitive =true)
            .RunAsync(CancellationToken.None);
      }

      #endregion
   }

}