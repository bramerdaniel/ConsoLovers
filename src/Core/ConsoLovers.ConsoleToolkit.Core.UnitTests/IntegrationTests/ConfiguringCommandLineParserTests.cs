// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfiguringCommandLineParserTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ConfiguringCommandLineParserTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureCommandLinePipelineCanBeCaseSensitive()
   {
      var application = CreateApplication<Args>(p => p.CaseSensitive = true)
         .Run("name=Robert");
      application.Arguments.Name.Should().Be(null);

      application = CreateApplication<Args>(p => p.CaseSensitive = false)
         .Run("name=Robert");
      application.Arguments.Name.Should().Be("Robert");
   }

   [TestMethod]
   public void EnsureCommandLinePipelineCanBeCaseSensitiveAndWorksCorrectly()
   {
      var application = CreateApplication<Args>(p => p.CaseSensitive = true)
         .Run("Name=Robert");
      application.Arguments.Name.Should().Be("Robert");
   }

   #endregion

   #region Methods

   private static IApplicationBuilder<T> CreateApplication<T>(Action<ICommandLineOptions> configurationAction)
      where T : class
   {
      return ConsoleApplication.WithArguments<T>()
         .ConfigureCommandLineParser(configurationAction);
   }

   #endregion

   public class Args
   {
      #region Public Properties

      [Argument("Name")]
      public string Name { get; set; }

      #endregion
   }
}