namespace CommandLineEngineDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class CustomizedArgumentsHelpCommand : ICommand<CustomizedHelpArgs>
   {
      #region ICommand Members

      public void Execute()
      {
         Console.WriteLine($"CommandName = CustomizedArgumentsHelpCommand, Path= {Arguments.Path}");
         Console.ReadLine();
      }

      #endregion

      #region ICommand<ExecuteArgs> Members

      public CustomizedHelpArgs Arguments { get; set; }

      #endregion
   }
}