namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   public interface ICommandVerification
   {
      void Execute(string commandName);

      void Argument(string name, object value);
   }
}