// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMessageBox.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core.Contracts;

   public class ConsoleMessageBox
   {
      private readonly IConsole console;

      public ConsoleMessageBox()
         : this(ColoredConsole.Instance)
      {
      }

      internal ConsoleMessageBox(IConsole console)
      {
         this.console = console;
      }

      public int Height { get; set; } = 10;
      public Margin Margin { get; set; } = new Margin(5);

      public ConsoleMessageBoxResult Show(string text)
      {
         console.Clear();
         var totalWidth = 50;
         for (int i = 0; i < Margin.Top; i++)
            console.WriteLine();
         //console.Write("╔".PadRight(totalWidth, '═'));
         //console.WriteLine("╗");

         console.Write(string.Empty.PadRight(Margin.Left));
         console.Write("█".PadRight(totalWidth, '█'));
         console.WriteLine("█");

         for (int i = 0; i < Height; i++)
         {
            console.Write(string.Empty.PadRight(Margin.Left));
            console.Write("█".PadRight(totalWidth, ' '));
            console.WriteLine("█");
         }

         console.Write(string.Empty.PadRight(Margin.Left));

         console.Write("█".PadRight(totalWidth, '█'));
         console.WriteLine("█");

         for (int i = 0; i < 1; i++)
         {
            console.Write(string.Empty.PadRight(Margin.Left));
            console.Write("█".PadRight(totalWidth, ' '));
            console.WriteLine("█");
         }

         console.Write(string.Empty.PadRight(Margin.Left));
         console.Write("█".PadRight(totalWidth - 20, ' '));
         console.Write("OK");
         console.WriteLine("█".PadLeft(19));

         for (int i = 0; i < 1; i++)
         {
            console.Write(string.Empty.PadRight(Margin.Left));
            console.Write("█".PadRight(totalWidth, ' '));
            console.WriteLine("█");
         }

         console.Write(string.Empty.PadRight(Margin.Left));

         console.Write("█".PadRight(totalWidth, '█'));

         console.WriteLine("█");


         return ConsoleMessageBoxResult.None;
      }

   }

   public class Margin
   {
      public Margin(int all)
      {
         Left = 5;
         Top = 5;
         Right = 5;
         Bottom = 5;
      }

      public int Left { get; set; }
      public int Top { get; set; }
      public int Right { get; set; }
      public int Bottom { get; set; }
   }

   public enum ConsoleMessageBoxResult
   {
      None = 0,
      OK = 1,
      Cancel = 2,
      Yes = 6,
      No = 7,
   }
}