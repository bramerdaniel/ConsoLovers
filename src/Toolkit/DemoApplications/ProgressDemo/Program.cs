// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ProgressDemo
{
   using System;
   using System.Threading;

   class Program
   {
      #region Methods

      static void Main(string[] args)
      {
         Console.WriteLine("Here is some progress");

         var progress = new ConsoleProgress(){ LeftMargin = 2 };

         for (int i = 0; i <= 1000; i++)
         {
            progress.Update((double)i/10, "Hello world");
            Thread.Sleep(5);
         }

         Console.ReadKey();
      }

      #endregion
   }
}