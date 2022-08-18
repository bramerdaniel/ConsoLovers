// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationArgumentsDependencyInjectionTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.DIContainer;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ApplicationArgumentsDependencyInjectionTests
{

   [TestMethod]
   public async Task EnsureServicesAreInjectedIntoApplicationArgsCorrectly()
   {
      var application = await ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddTransient<Service>())
         .RunAsync(string.Empty, CancellationToken.None);

      application.Arguments.Service.Should().NotBeNull();
   }

   [UsedImplicitly]
   private class Application : ConsoleApplication<ApplicationArgs>
   {
      public Application(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }

   internal class Service
   {
      public string Text { get; } = "Service";
   }

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      internal Service Service { get; }

      [InjectionConstructor]
      public ApplicationArgs(Service service)
      {
         Service = service;
      }
   }

}