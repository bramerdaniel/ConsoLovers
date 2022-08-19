// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMapperSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using Microsoft.Extensions.DependencyInjection;

public class CommandMapperSetup<T> : SetupBase<CommandMapper<T>>
   where T : class
{
   private readonly ServiceCollection serviceCollection = new();

   protected override CommandMapper<T> CreateInstance()
   {
      var serviceProvider = new DefaultServiceProvider(serviceCollection);
      return new CommandMapper<T>(serviceProvider, new ArgumentReflector());
   }

   public CommandMapperSetup<T> WithDefaults()
   {
      serviceCollection.AddRequiredServices()
         .AddArgumentTypes<T>();

      return this;
   }
   public CommandMapperSetup<T> AddArgumentTypes()
   {
      serviceCollection.AddArgumentTypes<T>();
      return this;
   }
}