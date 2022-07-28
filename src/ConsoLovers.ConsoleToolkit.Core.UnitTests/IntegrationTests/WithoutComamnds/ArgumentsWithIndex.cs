// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentsWithIndex.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests.WithoutComamnds
{
   using System.Diagnostics.CodeAnalysis;
   using System.Reflection.Emit;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ArgumentsWithIndex
   {
      [TestMethod]
      public void EnsureQuotePathWorkCorrectly()
      {
         using (var testContext = new ApplicationTestContext<SimpleArgs>())
         {
            var path = "C:\\SomeDirectory\\SomeFile.txt";
            testContext.RunApplication($"\"{path}\"");

            testContext.Application.Verify(a => a.RunAsync(), Times.Once);
            testContext.Application.Verify(a => a.RunWithAsync(It.Is<SimpleArgs>(x => x.Path == path)), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(It.IsAny<ICommand>()), Times.Never);
            
            testContext.Application.Verify(a => a.MappedCommandLineParameter("Path", path), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureTwoQuotePathsWorkCorrectly()
      {
         using (var testContext = new ApplicationTestContext<SimpleArgs>())
         {
            var path = "C:\\SomeDirectory\\SomeFile.txt";
            var secondPath = "C:\\SomeOther\\SomeOther.txt";
            testContext.RunApplication($"\"{path}\" \"{secondPath}\"");

            testContext.Application.Verify(a => a.RunAsync(), Times.Once);
            testContext.Application.Verify(a => a.RunWithAsync(It.Is<SimpleArgs>(x => x.Path == path && x.SecondPath == secondPath)), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(It.IsAny<ICommand>()), Times.Never);
            
            testContext.Application.Verify(a => a.MappedCommandLineParameter("Path", path), Times.Once);
            testContext.Application.Verify(a => a.MappedCommandLineParameter("SecondPath", secondPath), Times.Once);
         }
      }

      [TestMethod]
      public void EnsureNotQuotePathIsTreatedAsNamedParameter()
      {
         using (var testContext = new ApplicationTestContext<SimpleArgs>())
         {
            var path = "C:\\SomeDirectory\\SomeFile.txt";
            testContext.RunApplication(path);

            testContext.Application.Verify(a => a.RunAsync(), Times.Once);
            testContext.Application.Verify(a => a.RunWithAsync(It.Is<SimpleArgs>(x => x.Path == null)), Times.Once);
            testContext.Application.Verify(a => a.RunWithCommand(It.IsAny<ICommand>()), Times.Never);

            testContext.Application.Verify(a => a.Argument("Path", null), Times.Once);
            testContext.Application.Verify(a => a.UnmappedCommandLineParameter("C", "\\SomeDirectory\\SomeFile.txt"), Times.Once);
         }
      }
   }

   public class SimpleArgs
   {
      [Argument(0)]
      public string Path { get; set; }

      [Argument(1)]
      public string SecondPath { get; set; }
   }
}