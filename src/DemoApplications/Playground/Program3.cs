// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program3.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public static class Program3
   {
      #region Methods

      private static void Main(string[] args)
      {
         ConsoleApplicationManager.For<DeleteMeApplication>()
            .Run(args);
      }

      #endregion
   }

   [ConsoleWindowHeight(80)]
   [ConsoleWindowWidth(40)]
   internal class DeleteMeApplication : ConsoleApplicationWith<DeleteMeArguments>
   {
      #region Constructors and Destructors

      public DeleteMeApplication(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }

      #endregion

      #region Public Methods and Operators

      protected override void OnUnhandledCommandLineArgument(object sender, UnhandledCommandLineArgumentEventArgs e)
      {
      }

      public override void RunWith(DeleteMeArguments arguments)
      {
         if (arguments.WaitForDebugger)
            Console.ReadLine();

         Console.WriteLine("Doing some hard stuff");
         Task.Delay(3000).Wait();
      }

      #endregion
   }

   internal class DeleteMeArguments : ICommand<ExecuteArgs>
   {
      #region ICommand Members

      void ICommand.Execute()
      {
      }

      #endregion

      #region ICommand<ExecuteArgs> Members

      public ExecuteArgs Arguments { get; set; }

      #endregion

      #region Public Properties

      [Command(IsDefaultCommand = true)]
      public DeleteMeArguments Execute { get; set; }

      [Option("WaitForKey", "w")]
      public bool WaitForDebugger { get; set; }

      #endregion
   }

   internal class ExecuteArgs
   {
      #region Public Properties

      [Argument("Path", "p")]
      public string Path { get; set; }

      #endregion
   }
}