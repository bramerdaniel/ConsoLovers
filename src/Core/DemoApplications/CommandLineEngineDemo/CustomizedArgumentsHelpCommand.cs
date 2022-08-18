namespace CommandLineEngineDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class CustomizedArgumentsHelpCommand : CommandBase<CustomizedHelpArgs>
   {
      #region ICommand Members

      protected override void ExecuteOverride()
      {
         Console.WriteLine($"CommandName = CustomizedArgumentsHelpCommand, Path= {Arguments.Path}");
         Console.ReadLine();
      }

      #endregion
   }
}