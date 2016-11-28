using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoxDemo
{
   using ConsoLovers.ConsoleToolkit;

   class Program 
   {
      static void Main(string[] args)
      {
         ConsoleMessageBoxResult result = ConsoleMessageBoxResult.None;
         while (result != ConsoleMessageBoxResult.Yes)
         {
            result = new ConsoleMessageBox().Show("Do you want to exit the application ?");
            if (result != ConsoleMessageBoxResult.Yes)
            {
               Console.WriteLine("Ok then lets try it again");
               Console.ReadLine();
            }
         }


      }
   }
}
