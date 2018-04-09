// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineFactorySetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class EngineFactorySetup
   {
      #region Public Methods and Operators

      public DefaultFactory Done()
      {
         return new DefaultFactory();
      }

      #endregion
   }
}