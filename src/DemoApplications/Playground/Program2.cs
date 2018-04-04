// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program2.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Contracts;

   public static class Program2
   {
      private static void Main(string[] args)
      {
         var application = ConsoleApplicationManager.For<TheApplication>()
            .SetWindowWidth(150)
            .SetWindowHeight(40)
            .Run(args);
         if (application.Arguments.WaitForKey)
         {
            Console.WriteLine("Waiting for key");
            Console.ReadLine();
         }
      }
   }

   [ConsoleWindowHeight(80)]
   [ConsoleWindowWidth(40)]
   internal class TheApplication : ConsoleApplicationWith<TheArguments>
   {

      public override void RunWith(TheArguments arguments)
      {
         Console.WriteLine("Doing some hard stuff");
         Task.Delay(3000).Wait();
      }

      public TheApplication(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }

   internal class TheArguments
   {
      [Option("WaitForKey", "w")]
      public bool WaitForKey{ get; set; }
   }
}