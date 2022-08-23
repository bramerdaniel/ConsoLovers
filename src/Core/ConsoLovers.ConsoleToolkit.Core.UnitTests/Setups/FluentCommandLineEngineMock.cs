// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentCommandLineEngineMock.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.Core;

   using Moq;

   public class FluentCommandLineEngineMock
   {
      #region Public Methods and Operators

      public ICommandLineEngine Done()
      {
         return new Mock<ICommandLineEngine>().Object;
      }

      #endregion
   }
}