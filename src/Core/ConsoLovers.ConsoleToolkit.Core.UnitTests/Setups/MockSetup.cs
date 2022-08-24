// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   internal class MockSetup
   {
      #region Methods

      internal FluentCommandLineEngineMock CommandLineEngine()
      {
         return new FluentCommandLineEngineMock();
      }

      internal MiddlewareMockSetup<T> Middleware<T>()
         where T : class
      {
         return new MiddlewareMockSetup<T>();
      }

      #endregion
   }
}