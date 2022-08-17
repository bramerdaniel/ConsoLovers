// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineEngineSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.BootStrappers;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using Microsoft.Extensions.DependencyInjection;

   public class CommandLineEngineSetup : SetupBase<CommandLineEngine>
   {
      private readonly ServiceCollection serviceCollection = new();

      #region Methods

      protected override CommandLineEngine CreateInstance()
      {
         var objectFactory = new DefaultServiceProvider(serviceCollection);
         return new CommandLineEngine(objectFactory, new CommandExecutor());
      }

      #endregion

      public CommandLineEngineSetup AddTransient<T>()
         where T : class
      {
         serviceCollection.AddTransient<T>();
         return this;
      }

      public CommandLineEngineSetup AddArgumentTypes<T>()
         where T : class, new()
      {
         serviceCollection.AddArgumentTypes<T>();
         return this;
      }
   }
}