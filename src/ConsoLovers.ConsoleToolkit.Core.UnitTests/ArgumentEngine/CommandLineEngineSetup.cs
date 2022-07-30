// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineEngineSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   public class CommandLineEngineSetup : SetupBase<CommandLineEngine>
   {
      #region Methods

      protected override CommandLineEngine CreateInstance()
      {
         var objectFactory = new DefaultFactory();
         return new CommandLineEngine(objectFactory, objectFactory.Resolve<ICommandExecutor>());
      }

      #endregion
   }
}