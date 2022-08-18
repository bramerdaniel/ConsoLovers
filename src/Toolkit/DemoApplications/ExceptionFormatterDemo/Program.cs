using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionFormatterDemo
{
   using ConsoLovers.ConsoleToolkit.Core;

   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            throw new AccessViolationException("Go home programmer");
         }
         catch (Exception ex)
         {
            new ExceptionFormatter().Print(ex);
         }
         finally
         {
            Console.ReadLine();
         }
      }
   }
}
