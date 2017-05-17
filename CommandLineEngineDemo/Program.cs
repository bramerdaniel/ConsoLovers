// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   class Program : ConsoleApplicationWith<Commands>
   {
      #region Public Methods and Operators

      public override void RunWith(Commands arguments)
      {
         if (arguments.Wait)
         {
            Console.WriteLine("Waiting for another key key");
            Console.ReadLine();
         }
      }

      #endregion

      #region Methods

      static void Main(string[] args)
      {
         ConsoleApplicationManager.RunThis(args);
      }

      #endregion
   }

   internal class Commands
   {
      #region Public Properties

      [Command("Execute", "e")]
      [HelpText("None", "Executes the command.")]
      public ExecuteCommand Execute { get; set; }

      [Command("Help", "?", IsDefaultCommand = true)]
      [HelpText("None", "Displays the help you are watching at the moment.")]
      public HelpCommand Help { get; set; }

      [Option("Wait", "w")]
      [HelpText("None", "Waits for key press")]
      public bool Wait { get; set; }

      #endregion
   }

   internal class HelpCommand : ICommand
   {
      private readonly IEngineFactory factory;

      public HelpCommand(IEngineFactory factory)
      {
         this.factory = factory;
      }

      #region Constants and Fields

      #endregion

      #region ICommand Members

      public void Execute()
      {
         Console.WriteLine("CommandName = Help");
         new CommandLineEngine(factory). PrintHelp<Commands>(null);
         Console.ReadLine();
      }

      #endregion
   }

   internal class ExecuteCommand : ICommand<ExecuteArgs>
   {
      #region Constants and Fields

      #endregion

      #region ICommand Members

      public void Execute()
      {
         Console.WriteLine($"CommandName = Execute, Path= {Arguments.Path}");
         Console.ReadLine();
      }

      #endregion

      #region ICommand<ExecuteArgs> Members

      public ExecuteArgs Arguments { get; set; }

      #endregion
   }

   internal class ExecuteArgs
   {
      #region Public Properties

      [Command("Path", "p")]
      public string Path { get; set; }

      #endregion
   }
}