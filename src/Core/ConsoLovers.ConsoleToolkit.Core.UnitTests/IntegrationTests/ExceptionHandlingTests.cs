// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ExceptionHandlingTests
{
   [TestMethod]
   public async Task EnsureCommandIsExecutedCorrectly()
   {
      var customHandler = new Mock<IExceptionHandler>();

      var application = await ConsoleApplication.WithArguments<Args>()
         .ShowHelpWithoutArguments()
         .RunAsync("run parameter=ok", CancellationToken.None);

   }

   public class Args
   {
   }
}