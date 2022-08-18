namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   using System.Diagnostics.CodeAnalysis;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests.WithoutComamnds;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class RunTests : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureRunIsCalledOnRunable()
      {

         var runned = new ConsoleApplicationManagerGeneric<Runable>()
            .RunAsync(new string[0], CancellationToken.None).GetAwaiter().GetResult();

         runned.Mock.Verify(x => x.RunAsync(CancellationToken.None), Times.Once);
      }

      [TestMethod]
      public void EnsureRunInitializesWhenRequired()
      {
         var args = string.Empty;
         var runned = new ConsoleApplicationManagerGeneric<ApplicationWithArguments>().RunAsync(args, CancellationToken.None)
            .GetAwaiter().GetResult();

         runned.Args.Should().BeSameAs(args);
         runned.TestParameters.Should().NotBeNull();

         runned.Mock.Verify(x => x.RunAsync(CancellationToken.None), Times.Once);
      }

      private class ApplicationWithArguments : IApplication, IArgumentInitializer<TestArguments>
      {
         public Mock<IApplication> Mock { get; } = new Mock<IApplication>();

         public TestArguments TestParameters { get; private set; }

         public string Args{ get; private set; }

         public Mock<IArgumentInitializer<TestArguments>> Initializer { get; } = new Mock<IArgumentInitializer<TestArguments>>();

         public ApplicationWithArguments()
         {
            Initializer.Setup(x => x.CreateArguments()).Returns(TestParameters);
         }


         public void Run()
         {
            Mock.Object.Run();
         }

         public Task RunAsync(CancellationToken cancellationToken)
         {
            return Mock.Object.RunAsync(cancellationToken);
         }

         public TestArguments CreateArguments()
         {
            TestParameters = new TestArguments();
            return TestParameters;
         }

         public void InitializeFromString(TestArguments instance, string args)
         {
            TestParameters.Should().BeSameAs(instance);
            Args = args;
         }

         public void InitializeFromArray(TestArguments instance, string[] args)
         {
            TestParameters.Should().BeSameAs(instance);
            Args = string .Join(" ", args);
         }
      }

      private class TestArguments
      {
      }

      private class Runable : IApplication
      {
         public Mock<IApplication> Mock { get; } = new Mock<IApplication>();

         public void Run()
         {
            Mock.Object.Run();
         }

         public Task RunAsync(CancellationToken cancellationToken)
         {
            return Mock.Object.RunAsync(cancellationToken);
         }
      }

      #endregion
   }
}