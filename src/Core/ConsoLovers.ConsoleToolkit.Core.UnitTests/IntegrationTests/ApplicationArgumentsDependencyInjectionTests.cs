// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationArgumentsDependencyInjectionTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ApplicationArgumentsDependencyInjectionTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureServicesAreInjectedIntoApplicationArgsCorrectly()
   {
      var application = ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddSingleton(typeof(Service))
         .Run();

      application.Should().NotBeNull();
      application.Arguments.Service.Should().NotBeNull();
   }

   #endregion

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      #region Constructors and Destructors

      [InjectionConstructor]
      public ApplicationArgs([JetBrains.Annotations.NotNull] Service service)
      {
         Service = service ?? throw new ArgumentNullException(nameof(service));
      }

      #endregion

      #region Properties

      internal Service Service { get; }

      #endregion
   }

   internal class Service
   {
   }
}