namespace XCopyApplication
{
   using System;
   using System.IO;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class XCopyApplication : ConsoleApplication<XCopyArguments>
   {
      public XCopyApplication(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }

      public override bool HandleException(Exception exception)
      {
         if (exception is FileNotFoundException)
            Environment.Exit(-1);

         if (exception is PathTooLongException)
            Environment.Exit(-2);

         // e.g. do some logging for unknown exceptions...
         Environment.Exit(666);
         return true;
      }

      public override void RunWith(XCopyArguments arguments)
      {
      }
   }
}