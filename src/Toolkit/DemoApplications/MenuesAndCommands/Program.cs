// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands
{
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
            .UseMenuFor(typeof(AppArguments))
            .RunAsync(CancellationToken.None);
      }

      #endregion
   }

}