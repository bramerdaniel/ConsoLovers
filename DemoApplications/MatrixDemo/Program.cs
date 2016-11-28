using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixDemo
{
   using ConsoLovers.ConsoleToolkit.Utils;

   class Program
   {
      static void Main(string[] args)
      {
         Console.ForegroundColor = ConsoleColor.Red;
         var matrixEffect = new MatrixEffect
         {
            Text = $"You entered the matrix !{Environment.NewLine}Take the red to pill you leave{Environment.NewLine}or the blue pill to stay{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Test",
         };

         matrixEffect.StartAsync();
         matrixEffect.Wait();


         matrixEffect = new MatrixEffect
         {
            Text = $"You entered the matrix !{Environment.NewLine}Take the red to pill you leave{Environment.NewLine}or the blue pill to stay{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Test",
            
            AdditionalColor = ConsoleColor.Cyan,
            MatrixColor = ConsoleColor.DarkBlue,
            MatrixDrawingColor = ConsoleColor.Blue,
            TextForeground = ConsoleColor.Red

         };

         matrixEffect.StartAsync();
         matrixEffect.Wait();

         Console.WriteLine("finished");
         Console.ReadLine();
      }
   }
}
