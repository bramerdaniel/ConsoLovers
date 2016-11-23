// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.ComponentModel;
   using System.Linq;
   using System.Threading;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class Program : IRunable,  IArgumentInitializer<Arguments>, IExeptionHandler
   {
      private Arguments arguments;

      #region Methods

      private static void Main(string[] args)
      {
         ConsoleApplicationManager.Run<OnlyInterfacesUsed>(args);
         ConsoleApplicationManager.Run<AppAndParameters>(args);
         ConsoleApplicationManager.RunThis(args);
      }

      #endregion

      public void Run()
      {
         Console.WriteLine("Application is running with path : " + arguments.Path);
         for (int i = 0; i < 5; i++)
         {
            Console.WriteLine("Some complex logic");
            Thread.Sleep(300);
         }

         throw new InvalidOperationException("The developer of this complex logic did not expect this error");
      }

      public Arguments CreateArguments()
      {
         arguments = new Arguments();
         return arguments;
      }

      public void Initialize(Arguments instance ,string[] args)
      {
         instance.Path = args.FirstOrDefault();
      }

      public bool ExceptionHandled(Exception exception)
      {
         return true;
      }
   }

   internal class OnlyInterfacesUsed : IRunable, IArgumentInitializer<OnlyInterfacesUsed>
   {
      [Argument("Path")]
      public string  Path { get; set; }

      public void Run()
      {
         Console.WriteLine("Application is running with path: " + Path);
         Console.ReadLine();
      }

      public OnlyInterfacesUsed CreateArguments()
      {
         return this;
      }

      public void Initialize(OnlyInterfacesUsed instance, string[] args)
      {
         instance.Path = args.FirstOrDefault();
      }
   }

   internal class AppAndParameters : ConsoleApplicationWith<AppAndParameters>
   {
      [Argument("Path")]
      public string Path { get; set; }

      public override void Run()
      {
         Console.WriteLine("Application is running with path: " + Path);
         Console.ReadLine();
      }
   }
}