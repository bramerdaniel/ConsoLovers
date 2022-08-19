// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.CommandExtensions;
   using ConsoLovers.ConsoleToolkit.Core;

   public static class Program
   {
      #region Methods

      private static async Task Main()
      {
         var application = await ConsoleApplicationManager.For<Application>()
            .UseMenuWithoutArguments(menu =>
            {
               menu.Header = "Hello Menu";
               menu.CloseKeys = new[] { ConsoleKey.Escape };
            })
            .RunAsync(CancellationToken.None);
      }

      #endregion
   }

}