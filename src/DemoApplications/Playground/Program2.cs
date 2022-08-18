// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program2.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public static class Program2
   {
      #region Methods

      private static void Main(string[] args)
      {
         var application = ConsoleApplicationManager.For<TheApplication>().SetWindowWidth(150).SetWindowHeight(40).Run(args);

         if (application.Arguments.WaitForDebugger)
         {
            Console.WriteLine("Waiting for key");
            Console.ReadLine();
         }
      }

      #endregion
   }

   [ConsoleWindowHeight(80)]
   [ConsoleWindowWidth(40)]
   internal class TheApplication : ConsoleApplication<TheArguments>
   {
      #region Constructors and Destructors

      public TheApplication(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }

      #endregion

      #region Public Methods and Operators
      
      public override async Task RunWithAsync(TheArguments arguments, CancellationToken cancellationToken)
      {
         if (arguments.WaitForDebugger)
            Console.ReadLine();

         Console.WriteLine("Doing some hard stuff");
         await Task.Delay(3000, cancellationToken);
      }

      #endregion
   }

   internal class TheArguments
   {
      [Argument("Path", "p")]
      public string Path { get; set; }

      [Option("WaitForKey", "w")]
      public bool WaitForDebugger { get; set; }
   }
}