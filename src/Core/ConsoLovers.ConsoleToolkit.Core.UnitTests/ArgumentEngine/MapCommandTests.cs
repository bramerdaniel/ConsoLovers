﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapCommandTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class MapCommandTests
   {
      #region Public Methods and Operators

      #endregion
   }

   internal class CommandTestClass
   {
      #region Public Properties

      [Command("Restore", "R")]
      public TestCommandType TheCommand { get; set; }

      #endregion
   }

   [Flags]
   internal enum TestCommandType
   {
      None = 0,

      Backup = 1,

      Init = 2,

      Restore = 4
   }
}