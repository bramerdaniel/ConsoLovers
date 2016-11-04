// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineTestBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class EngineTestBase
   {
      #region Methods

      protected ArgumentEngine GetTarget()
      {
         return Setup.ArgumentEngine().Done();
      }

      #endregion
   }
}