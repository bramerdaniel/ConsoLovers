namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.Setups;

   public class ArgumentMapperSetup
   {
      public ArgumentMapperSetup<T> ForType<T>()
         where T : class
      {
         return new ArgumentMapperSetup<T>();
      }
   }

   public class ArgumentMapperSetup<T>
      where T : class
   {
      public ArgumentMapper<T> Done()
      {
         return new ArgumentMapper<T>(Setup.EngineFactory().Done());
      }
   }
}