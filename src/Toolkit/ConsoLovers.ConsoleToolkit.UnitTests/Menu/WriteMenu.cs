// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.Menu
{
   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Menu;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   public class WriteMenu
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureStringHeaderIsDisplayed()
      {
         var header = "Some test header";

         var consoleMock = new Mock<IConsole>();
         var target = new ConsoleMenu(consoleMock.Object) { Header = header };

         target.RefreshMenu();

         consoleMock.Verify(x => x.Write(header), Times.Once());
         consoleMock.Verify(x => x.WriteLine(), Times.Once());
      }

      [TestMethod]
      public void EnsureStringFooterIsDisplayed()
      {
         var footer = "Some test footer";

         var consoleMock = new Mock<IConsole>();
         var target = new ConsoleMenu(consoleMock.Object) { Footer = footer };

         target.RefreshMenu();

         consoleMock.Verify(x => x.Write(footer), Times.Once());
         consoleMock.Verify(x => x.WriteLine(), Times.Once());
      }

      [TestMethod]
      public void EnsureFlatItemIsDisplayed()
      {
         var consoleMock = new Mock<IConsole>();
         var target = new ConsoleMenu(consoleMock.Object);
         target.Add(new ConsoleMenuItem("Item 1"));

         target.RefreshMenu();

         consoleMock.Verify(x => x.Write(target.Selector), Times.Once());
         consoleMock.Verify(x => x.Write("Item 1"), Times.Once());
         consoleMock.Verify(x => x.WriteLine(), Times.Once());
      }

      #endregion
   }
}