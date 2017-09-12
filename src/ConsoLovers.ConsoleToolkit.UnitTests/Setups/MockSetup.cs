// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.Setups
{
   public class MockSetup
   {
      #region Public Methods and Operators

      public FluentCommandLineEngineMock CommandLineEngine()
      {
         return new FluentCommandLineEngineMock();
      }

      #endregion
   }
}