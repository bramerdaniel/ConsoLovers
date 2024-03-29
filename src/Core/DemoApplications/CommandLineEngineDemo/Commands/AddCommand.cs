﻿namespace CommandLineEngineDemo.Commands
{
   using System.IO;

   internal class AddCommand : CommandBase<AddArguments>
   {
      protected override void BeforeExecute()
      {
         if (string.IsNullOrWhiteSpace(Arguments.Path))
            Arguments.Path = Directory.GetCurrentDirectory();
      }

      protected override void ExecuteOverride()
      {
         if (Arguments.Wait)
            Console.ReadLine();
      }
   }
}