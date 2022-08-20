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

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ArgumentsWithIndex
   {
      [TestMethod]
      public void EnsureQuotePathWorkCorrectly()
      {
         var path = "C:\\SomeDirectory\\SomeFile.txt";
         var application = ConsoleApplication.WithArguments<SimpleArgs>()
            .Run($"\"{path}\"");

         application.Arguments.Path.Should().Be(path);
      }

      [TestMethod]
      public void EnsureTwoQuotePathsWorkCorrectly()
      {
         var path = "C:\\SomeDirectory\\SomeFile.txt";
         var secondPath = "C:\\SomeOther\\SomeOther.txt";

         var application = ConsoleApplication.WithArguments<SimpleArgs>()
            .Run($"\"{path}\" \"{secondPath}\"");

         application.Arguments.Path.Should().Be(path);
         application.Arguments.SecondPath.Should().Be(secondPath);


      }

      [TestMethod]
      public void EnsureNotQuotePathIsTreatedAsNamedParameter()
      {
         var path = "C:\\SomeDirectory\\SomeFile.txt";
         var application = ConsoleApplication.WithArguments<SimpleArgs>()
            .Run(path);

         application.Arguments.Path.Should().Be(null);
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