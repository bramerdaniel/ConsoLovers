namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   public interface IApplicationVerification
   {
      string Run();
      string RunWithCommand();

      string RunWith();

      string RunWithoutArguments();

      void Argument(string name, object value);
   }
}