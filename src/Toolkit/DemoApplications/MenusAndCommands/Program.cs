// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Menu;

   public static class Program
   {
      #region Methods

      private static async Task Main()
      {
         await ConsoleApplication.WithArguments<AppArguments>()
            .UseMenuWithoutArguments(options =>
            {
               options.Menu.Header = new MenusAndCommands();
               options.Menu.CloseKeys = new[] { ConsoleKey.Escape };
               options.MenuBehaviour = MenuBuilderBehaviour.ShowAllCommand;

               options.Menu.Expander = new ExpanderDescription { Collapsed = "++", Expanded = "--" };
               options.Menu.CircularSelection = false;
               options.Menu.Selector = "►";
               options.Menu.IndexMenuItems = true;
               options.DefaultArgumentInitializationMode = ArgumentInitializationModes.WhileExecution;
            })
            .ConfigureCommandLineParser(o => o.CaseSensitive = true)
            .RunAsync(CancellationToken.None);
      }

      #endregion

      internal class MenusAndCommands : IMenuHeader
      {
         #region IMenuHeader Members

         public void PrintHeader(IConsole console)
         {
            console.WriteLine();
            console.WriteLine("Menus and commands", ConsoleColor.Cyan);
            console.WriteLine();
         }

         #endregion
      }
   }
}