namespace CommandLineEngineDemo
{
   using System;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   [HelpTextProvider(typeof(CustomizedHelpArgs))]
   internal class CustomizedHelpArgs : IHelpTextProvider
   {
      #region IHelpTextProvider Members

      public void Initialize(Type helpType, ResourceManager resourceManager)
      {
      }

      public void WriteArguments()
      {
         Console.WriteLine($"this help was creates by the {typeof(CustomizedHelpArgs).Name} class");
      }

      public void WriteFooter()
      {
      }

      public void WriteHeader()
      {
         Console.WriteLine("Help for the arguments of the custom arguments command");
         Console.WriteLine("----------------------------");
         Console.WriteLine();
      }

      public void WriteNoHelpAvailable()
      {
      }

      #endregion

      #region Public Properties

      [Argument("Path", "p", Required = true)]
      [HelpText("ExecuteArgs_PathHelp", "The path to the thing that should be executed.")]
      public string Path { get; set; }

      #endregion

      #region Public Methods and Operators

      public void Initialize(Type helpType, int availableWidth)
      {
      }

      #endregion
   }
}