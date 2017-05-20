// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineFactorySetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class EngineFactorySetup
   {
      #region Public Methods and Operators

      public EngineFactory Done()
      {
         return new EngineFactory();
      }

      #endregion
   }
}