﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineEngineSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.Builders;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using Microsoft.Extensions.DependencyInjection;

   using Moq;

   public class CommandLineEngineSetup : SetupBase<CommandLineEngine>
   {
      private readonly ServiceCollection serviceCollection = new();

      #region Methods

      protected override CommandLineEngine CreateInstance()
      {
         var serviceProvider = new Container(serviceCollection);
         var argumentReflector = new ArgumentReflector();
         return new CommandLineEngine(serviceProvider, new CommandLineArgumentParser(), argumentReflector);
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

      public CommandLineEngineSetup WithDefaults()
      {
         serviceCollection.AddRequiredServices();
         return this;
      }
   }
}