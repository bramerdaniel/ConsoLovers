// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineTestBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   public class EngineTestBase
   {
      #region Methods

      protected CommandLineEngine GetTarget()
      {
         return Setup.CommandLineEngine().Done();
      }

      #endregion
   }
}