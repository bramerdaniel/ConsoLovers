// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentCommandLineEngineMock.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

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