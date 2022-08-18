// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDependencyInjectionTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ApplicationDependencyInjectionTests
{
   [TestMethod]
   public async Task EnsureServicesAreInjectedIntoCommandsCorrectly()
   {
      var application = await ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(new Service("IntoTheApp")))
         .RunAsync(string.Empty, CancellationToken.None);

      application.Service.Should().NotBeNull();
      application.Service.Text.Should().Be("ChangedByApp");
   }

   [UsedImplicitly]
   private class Application : ConsoleApplication<ApplicationArgs>
   {
      protected override Task RunWithoutArgumentsAsync(CancellationToken cancellationToken)
      {
         Service.Text = "ChangedByApp";
         return Task.CompletedTask;
      }

      internal Service Service { get; }

      public Application(ICommandLineEngine commandLineEngine, Service service)
         : base(commandLineEngine)
      {
         Service = service;
      }

   }

   internal class Service
   {
      public Service(string text)
      {
         Text = text ?? throw new ArgumentNullException(nameof(text));
      }

      public string Text { get; set; }
   }

   [UsedImplicitly]
   internal class ApplicationArgs
   {
   }

}