namespace SectionUpdate
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;

   internal class Program
   {
      static IConsole console = new ConsoleProxy();

      static void Main(string[] args)
      {
         console.Write("Number: ");
         var section = console.CreateFixedSegment(5);
         console.Write("xxxxx");
         var reverse = console.CreateFixedSegment(10);
         console.Write("xxxxx");
         console.WriteLine();
         Thread.Sleep(1000);
         console.WriteLine("That is how it's done", ConsoleColor.Cyan);
         Task.Run(() => UpdateSection(section));
         Task.Run(() => UpdateSectionReverse(reverse));
         Task.Run(() => WriteLines());

         Console.ReadLine();
      }

      private static void WriteLines()
      {
         for (int i = 0; i <= 25; i++)
         {
            console.WriteLine("This is a line");
            Thread.Sleep(100);
         }

      }

      private static async Task UpdateSection(IFixedSegment segment)
      {
         for (int i = 0; i <= 100; i++)
         {
            segment.Update($"{i}", GetColor(i));
            await Task.Delay(50);
         }
      }
      private static async Task UpdateSectionReverse(IFixedSegment segment)
      {
         for (int i = 100; i > 0; i--)
         {
            segment.Update($"äüö {i}", GetColor(i));
            await Task.Delay(50);
         }
      }

      private static ConsoleColor GetColor(int i)
      {
         if (i < 20)
            return console.ForegroundColor;

         if (i < 40)
            return ConsoleColor.Yellow;

         if (i < 60)
            return ConsoleColor.DarkMagenta;

         if (i < 80)
            return ConsoleColor.Blue;

         return ConsoleColor.Green;
      }
   }
}