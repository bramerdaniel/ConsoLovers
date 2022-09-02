// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;
   using System.Diagnostics;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core;

   class Program
   {
      #region Methods

      static void Main()
      {
         var program = ConsoleApplication.WithArguments<ApplicationArguments>()
            .AddResourceManager(Properties.Resources.ResourceManager)
            .Run();

         // PrintArgs(program.Arguments);

         if (Debugger.IsAttached)
            Console.ReadLine();

         if (program.Arguments != null && program.Arguments.Wait)
         {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
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


      #endregion

   }
}