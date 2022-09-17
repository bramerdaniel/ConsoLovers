namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public static class Program
{
   public static void Main()
   {
      var executable = ConsoleApplication.WithArguments<ApplicationArgs>()
         //.UseApplicationLogic(typeof(MyCustomLogic))
         .Run();
         //.UseServiceProviderFactory(new DefaultServiceProviderFactory())
         // .UseApplicationLogic(Execute) 
         //.AddMiddleware(typeof(TryCatchMiddleware))
         //.AddMiddleware<ApplicationArgs, RepeatMiddleware>()
         // .Run();
         //.ConfigureMapping(o =>
         //{
         //   o.UnhandledArgumentsBehavior = UnhandledArgumentsBehaviors.UseCustomHandler | UnhandledArgumentsBehaviors.LogToConsole;
         //})
         //.AddService(x => x.AddSingleton<IMappingHandler<ApplicationArgs>>(new Handler()))
         //.Run(t => throw new InvalidOperationException("No command could be was executed"));

      Console.ReadLine();
   }
}