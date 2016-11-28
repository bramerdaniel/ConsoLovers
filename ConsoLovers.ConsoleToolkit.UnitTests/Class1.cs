using System.Diagnostics.CodeAnalysis;

using ConsoLovers.UnitTests.ArgumentEngine;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoLovers.UnitTests
{
   using ConsoLovers.ConsoleToolkit;

   using FluentAssertions;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class RunTests : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureRunIsCalledOnRunable()
      {
         var runned = new ConsoleApplicationManager<Runable>().Run(new string[0]);

         runned.Mock.Verify(x => x.Run(), Times.Once);
      }

      [TestMethod]
      public void EnsureRunInitializesWhenRequired()
      {
         var args = new string[0];
         var runned = new ConsoleApplicationManager<ApplicationWithArguments>().Run(args);

         runned.Args.Should().BeSameAs(args);
         runned.TestParameters.Should().NotBeNull();

         runned.Mock.Verify(x => x.Run(), Times.Once);
      }

      private class ApplicationWithArguments : IApplication, IArgumentInitializer<TestArguments>
      {
         public Mock<IApplication> Mock { get; } = new Mock<IApplication>();

         public TestArguments TestParameters { get; private set; }

         public string[] Args{ get; private set; }

         public Mock<IArgumentInitializer<TestArguments>> Initializer { get; } = new Mock<IArgumentInitializer<TestArguments>>();

         public ApplicationWithArguments()
         {
            Initializer.Setup(x => x.CreateArguments()).Returns(TestParameters);
         }

         public void Run()
         {
            Mock.Object.Run();
         }

         public TestArguments CreateArguments()
         {
            TestParameters = new TestArguments();
            return TestParameters;
         }

         public void Initialize(TestArguments instance, string[] args)
         {
            TestParameters.Should().BeSameAs(instance);
            Args = args;
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
      }

      #endregion
   }
}