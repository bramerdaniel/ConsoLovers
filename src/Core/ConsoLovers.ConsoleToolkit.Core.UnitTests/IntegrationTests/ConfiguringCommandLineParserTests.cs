// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfiguringCommandLineParserTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ConfiguringCommandLineParserTests
{
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

   private static IApplicationBuilder<T> CreateApplication<T>(Action<ICommandLineOptions> configurationAction)
      where T : class
   {
      return ConsoleApplication.WithArguments<T>()
            .ConfigureCommandLineParser(configurationAction);
   }

   public class Args
   {
      [Argument("Name")] 
      public string Name { get; set; }
   }
}