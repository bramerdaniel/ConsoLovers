// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMessageBox.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;

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

      public ConsoleMessageBoxResult Show(string text)
      {
         console.Clear();
         var totalWidth = 50;

         console.Write("╔".PadRight(totalWidth, '═'));
         console.WriteLine("╗");

         for (int i = 0; i < Height; i++)
         {
            console.Write("║".PadRight(totalWidth, ' '));
            console.WriteLine("║");
         }

         console.Write("╠".PadRight(totalWidth, '═'));
         console.WriteLine("╣");

         for (int i = 0; i < 3; i++)
         {
            console.Write("║");
            console.Write(string.Empty.PadRight(49, ' '));
            console.WriteLine("║");
         }

         console.Write("╚".PadRight(totalWidth, '═'));
         console.WriteLine("╝");

         console.Write("█");


         return ConsoleMessageBoxResult.None;
      }

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