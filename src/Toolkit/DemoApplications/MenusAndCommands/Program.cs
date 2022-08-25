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
   using MenusAndCommands.Model;
   using Microsoft.Extensions.DependencyInjection;

   public static class Program
   {
      #region Methods

      private static async Task Main()
      {
         await ConsoleApplication.WithArguments<AppArguments>()
            .AddService(s => s.AddSingleton<IUserManager, UserManager>())
            .UseMenuWithoutArguments(options =>
            {
               options.Menu.Header = new MenusAndCommands();
               options.Menu.CloseKeys = new[] { ConsoleKey.Escape };
               options.MenuBehaviour = MenuBuilderBehaviour.ShowAllCommand;

               options.Menu.Expander = new ExpanderDescription { Collapsed = " +", Expanded = " -" };
               options.Menu.CircularSelection = false;
               options.Menu.Selector = "  ";// "►";
               options.Menu.IndexMenuItems = true;
               options.Menu.IndentSize = 3;
               options.DefaultArgumentInitializationMode = ArgumentInitializationModes.WhileExecution;
            })
            .ConfigureCommandLineParser(o => o.CaseSensitive = true)
            .UseExceptionHandler(typeof(AllExceptionsHandler))
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

   internal class AllExceptionsHandler : IExceptionHandler
   {
      private readonly IConsole console;

      public AllExceptionsHandler(IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      public bool Handle(Exception exception)
      {
         console.WriteLine(exception.Message, ConsoleColor.Red);
         return true;
      }
   }
}