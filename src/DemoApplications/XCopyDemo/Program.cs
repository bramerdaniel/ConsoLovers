using System;

namespace XCopyDemo
{
   using System.IO;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   [ConsoleWindowWidth(120)]
   [ConsoleWindowHeight(80)]
   class Program : ConsoleApplication<CopyArguments>
   {
      // Call it with different arguments
      // s=FileToCopy.txt d=HereItWasCopied.txt -q -o
      // s=FileToCopy.txt d=HereItWasCopied.txt -q
      // s=FileToCopy.txt

      static void Main(string[] args)
      {
         ConsoleApplicationManager.RunThis(args);
      }

      public override void RunWith(CopyArguments arguments)
      {
         if (!File.Exists(arguments.SourceFile))
            throw new XCopyException("Source file does not exist");

         if (File.Exists(arguments.DestinationFile))
         {
            if (arguments.Silent)
            {
               if (!arguments.OverrideExisting)
                  throw new XCopyException("Destination file allready exists. Use OverrideExisting option [-o] to override ");
            }
         }

         File.Copy(arguments.SourceFile, arguments.DestinationFile, arguments.OverrideExisting);
         Console.WriteLine("Copy of file completed successfully", ConsoleColor.DarkGreen);

         if (arguments.WaitForEnterKey)
            WaitForEnter();
      }

      public override bool HandleException(Exception exception)
      {
         if (base.HandleException(exception))
            return true;

         var error = exception as XCopyException;
         if (error != null)
         {
            Console.WriteLine(error.Message, ConsoleColor.Red);
            WaitForEnter();
            return true;
         }

         return false;
      }

      public Program(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }
}
