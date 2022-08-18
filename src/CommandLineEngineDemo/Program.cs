// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;
   using System.Reflection;
   using System.Resources;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   [ConsoleWindowWidth(140)]
   [ConsoleWindowHeight(60)]
   class Program : ConsoleApplication<ApplicationArguments>
   {
      #region Public Methods and Operators

      public override Task RunWithAsync(ApplicationArguments arguments, CancellationToken cancellationToken)
      {
         if (arguments.Wait)
         {
            Console.WriteLine("Waiting for another key to be pressed");
            Console.ReadLine();
         }

         return Task.CompletedTask;
      }

      #endregion

      #region Methods

      private new static readonly IConsole Console = new ConsoleProxy();

      static void Main()
      {
         var container = new Container();
         container.Register<ResourceManager>(Properties.Resources.ResourceManager).WithLifetime(Lifetime.Singleton);
         var objectFactory = new DefaultFactory(container);

         var program = ConsoleApplicationManager.For<Program>().UsingFactory(objectFactory).Run();
         PrintArgs(program.Arguments);
         if (program.Arguments != null && program.Arguments.Wait)
         {
            Console.WriteLine();
            program.WaitForEnter();
         }
      }

      private static void PrintArgs(ApplicationArguments args)
      {
         Console.WriteLine();
         if (args == null)
         {
            Console.WriteLine(" no application arguments", ConsoleColor.Yellow);
            return;
         }

         Console.WriteLine(" ### Application arguments ###");
         ConsoleColor color = ConsoleColor.White;
         foreach (var propertyInfo in args.GetType().GetProperties())
         {
            var commandLineAttribute = propertyInfo.GetCustomAttribute<CommandLineAttribute>();
            var value = propertyInfo.GetValue(args);
            if (value != null)
            {
               color = color == ConsoleColor.White ? ConsoleColor.Gray : ConsoleColor.White;
               Console.WriteLine($"  - {propertyInfo.Name,-10} = {value,-40} [Shared={commandLineAttribute?.Shared}]", color);
            }
         }
      }

      protected override void OnUnhandledCommandLineArgument(object sender, CommandLineArgumentEventArgs e)
      {
         Console.WriteLine($"Unknown command line argument '{e.Argument.Name}' at index {e.Argument.Index}.", ConsoleColor.Yellow);
         base.OnUnhandledCommandLineArgument(sender, e);
      }

      #endregion

      public Program(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }
}