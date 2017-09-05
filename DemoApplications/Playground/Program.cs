// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.IO;
   using System.Linq;
   using System.Threading;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;

   public class Program
   {
      #region Constants and Fields

      private Arguments arguments;

      #endregion

      #region Public Methods and Operators

      public Arguments CreateArguments()
      {
         arguments = new Arguments();
         return arguments;
      }

      public bool HandleException(Exception exception)
      {
         return true;
      }

      public void Initialize(Arguments instance, string[] args)
      {
         instance.Path = args.FirstOrDefault();
      }

      public void Run()
      {
         var left = new Random((int)DateTime.Now.Ticks);
         var top = new Random((int)DateTime.Now.Ticks + 200);
         var buffer = new ConsoleBuffer();
         buffer.ReadonlySections[10, 10] = true;
         buffer.ReadonlySections[11, 11] = true;
         buffer.ReadonlySections[11, 10] = true;
         buffer.ReadonlySections[10, 11] = true;

         while (true)
         {
            buffer.WriteLine(left.Next(0, Console.BufferWidth - 1), top.Next(0, Console.BufferHeight - 1), '#', ConsoleColor.Green, ConsoleColor.Blue);
         }

         Console.WriteLine("Application is running with path : " + arguments.Path);
         for (int i = 0; i < 5; i++)
         {
            Console.WriteLine("Some complex logic");
            Thread.Sleep(300);
         }

         throw new InvalidOperationException("The developer of this complex logic did not expect this error");
      }

      #endregion

      #region Methods

      private static void Main(string[] args)
      {
         string text = null;
         while (text != "exit")
         {
            text = new InputBox<string>("Enter some long text: ", "SomeText").ReadLine(10);
            Console.WriteLine(text);
         }

         new ConsoleProxy().WaitForKey(ConsoleKey.Escape);
         ConsoleApplicationManager.For<MyProgramLogic>().Run(args);
      }

      #endregion
   }

   internal class MyProgramLogic : ConsoleApplicationWith<MyArguments>
   {
      #region Public Methods and Operators

      /// <summary>Entry point for  non static logic.</summary>
      /// <param name="arguments">The arguments.</param>
      public override void RunWith(MyArguments arguments)
      {
         if (!File.Exists(arguments.Path))
            Console.WriteLine("Path must point to an existing file");

         // some cool logic...
      }

      #endregion

      public MyProgramLogic(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }

   // The arguments class your args are mapped to
   internal class MyArguments
   {
      #region Public Properties

      [Argument("Path", "p")]
      public string Path { get; set; }

      #endregion
   }

   internal class OnlyInterfacesUsed : IApplication, IArgumentInitializer<OnlyInterfacesUsed>
   {
      #region IApplication Members

      public void Run()
      {
         Console.WriteLine("Application is running with path: " + Path);
         Console.ReadLine();
      }

      #endregion

      #region IArgumentInitializer<OnlyInterfacesUsed> Members

      public OnlyInterfacesUsed CreateArguments()
      {
         return this;
      }

      public void InitializeArguments(OnlyInterfacesUsed instance, string[] args)
      {
         instance.Path = args.FirstOrDefault();
      }

      #endregion

      #region Public Properties

      [Argument("Path")]
      public string Path { get; set; }

      #endregion
   }

   internal class AppAndParameters : ConsoleApplicationWith<AppAndParameters>
   {
      #region Public Properties

      [Argument("Path")]
      public string Path { get; set; }

      #endregion

      #region Public Methods and Operators

      public override void RunWith(AppAndParameters arguments)
      {
         Console.WriteLine("Application is running with path: " + Path);
         Console.ReadLine();
      }

      #endregion

      public AppAndParameters(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }
}