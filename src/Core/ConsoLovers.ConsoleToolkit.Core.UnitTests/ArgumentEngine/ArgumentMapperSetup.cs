namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System.Security.Cryptography;

   using ConsoLovers.ConsoleToolkit.Core.Builders;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using Microsoft.Extensions.DependencyInjection;

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
         var serviceCollection = new ServiceCollection();
         serviceCollection.AddApplicationTypes<T>();
         return new ArgumentMapper<T>(new Container(serviceCollection));
      }
   }
}