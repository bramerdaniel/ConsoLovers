namespace CommandLineEngineDemo
{
   using System;
   using System.Reflection;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   [HelpTextProvider(typeof(CustomizedHelpArgs))]
   internal class CustomizedHelpArgs : IHelpProvider
   {
      #region IHelpTextProvider Members

      public void PrintHelp()
      {
         Console.WriteLine("Help for the arguments of the custom arguments command");
         Console.WriteLine("------------------------------------------------------");
         Console.WriteLine();
         Console.WriteLine($"this help was creates by the {typeof(CustomizedHelpArgs).Name} class");
      }

      #endregion

      #region Public Properties

      [Argument("Path", "p", Required = true)]
      [HelpText("The path to the thing that should be executed.", "ExecuteArgs_PathHelp")]
      public string Path { get; set; }

      #endregion

      #region Public Methods and Operators

      public void Initialize(Type helpType, int availableWidth)
      {
      }

      #endregion

      public void PrintTypeHelp(Type type)
      {
         PrintHelp();
      }

      public void PrintPropertyHelp(PropertyInfo property)
      {

         PrintHelp();
      }
   }
}