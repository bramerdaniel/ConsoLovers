// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System.Resources;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.DIContainer;

   class Program : ConsoleApplicationWith<Commands>
   {
      #region Public Methods and Operators


      public override void RunWith(Commands arguments)
      {
         if (arguments.Wait)
         {
            Console.WriteLine("Waiting for another key to be pressed");
            Console.ReadLine();
         }
      }

      #endregion

      #region Methods

      static void Main(string[] args)
      {
         var container = new Container();
         container.Register<ResourceManager>(Properties.Resources.ResourceManager).WithLifetime(Lifetime.Singleton);
         var objectFactory = new DefaultFactory(container);
         ConsoleApplicationManager.For<Program>().UsingFactory(objectFactory).Run(args);
         Console.ReadLine();
      }

      #endregion

      public Program(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }
}