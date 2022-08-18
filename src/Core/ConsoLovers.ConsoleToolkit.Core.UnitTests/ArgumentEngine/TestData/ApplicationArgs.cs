namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.TestData
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class ApplicationArgs
   {
      #region Public Properties

      [Command("execute")]
      public GenericCommand<ExecuteArgs> Execute { get; set; }

      #endregion
   }
}