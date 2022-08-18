using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCopyApplication
{
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core;

   class Program
   {
      static void Main(string[] args)
      {
         ConsoleApplicationManager.For<XCopyApplication>()
            .SetWindowTitle("XCopyApplication")
            .SetWindowWidth(100)
            .Run(args);

         Console.ReadLine();
      }
   }
}
