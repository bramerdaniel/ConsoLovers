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

      private static void Main()
      {
          ConsoleApplication.WithArguments<PromptsDemoArgs>()
            .Run();
      }

      #endregion

   }


}