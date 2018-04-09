// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoleBufferDemo
{
   using System;
   using System.Timers;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Core;

   [ConsoleWindowTitle("ConsoleBufferDemo")]
   class Program : ConsoleApplication
   {
      #region Constants and Fields

      private readonly ConsoleBuffer buffer = new ConsoleBuffer();

      private int firstColumn, secondColumn;

      private int direction = 1;

      string firstText = "THIS IS DIRECTLY WRITTEN THE BUFFER";

      string secondText = "WHILE YOU STILL CAN TYPE YOUR TEXT!";

      #endregion

      #region Public Methods and Operators

      public override void Run()
      {
         firstColumn = 0;
         secondColumn = Console.WindowWidth - firstText.Length;

         Timer timer = new Timer(200) { AutoReset = true };
         timer.Elapsed += OnTimerElapsed;
         timer.Start();



         Console.SetCursorPosition(0, 2);
         while (new InputBox<string>("Enter any text: ").ReadLine(4) != "exit")
            Console.WriteLine("Enter exit if you want to leave !", ConsoleColor.Cyan);
      }

      #endregion

      #region Methods

      static void Main(string[] args)
      {
         ConsoleApplicationManager.RunThis(args);
      }

      private void OnTimerElapsed(object sender, EventArgs e)
      {
         firstColumn += direction;
         secondColumn -= direction;

         if (firstColumn + firstText.Length > Console.WindowWidth)
         {
            direction = -1;

            firstColumn -= 2;
            secondColumn += 2;
         }

         if (firstColumn < 0)
         {
            direction = 1;

            firstColumn += 2;
            secondColumn -= 2;
         }

         buffer.WriteLine(firstColumn, 0, firstText, ConsoleColor.Green);
         buffer.WriteLine(secondColumn, 1, secondText, ConsoleColor.Red);
      }

      #endregion
   }
}