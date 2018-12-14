namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class CommandLinerParserSetup : Setups.SetupBase<CommandLineArgumentParser>
   {
      protected override CommandLineArgumentParser CreateInstance()
      {
         return new CommandLineArgumentParser();
      }
   }
}