namespace ConsoLovers.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.DIContainer;

   using Moq;

   internal class ApplicationTestContext<T> : IDisposable
   {
      private Container container;

      public ApplicationTestContext()
      {
         container = new Container();

         Commands = new Mock<ICommandVerification>();
         Application = new Mock<IApplicationVerification>();

         container.Register<ICommandVerification>(Commands.Object);
         container.Register<IApplicationVerification>(Application.Object);

         Factory = new DefaultFactory(container);
      }

      public DefaultFactory Factory { get; }

      public Mock<ICommandVerification> Commands { get; }

      public Mock<IApplicationVerification> Application { get; }

      public void RunApplication(params string[] args)
      {
         ConsoleApplicationManager
            .For<T>()
            .UsingFactory(Factory)
            .Run(args);
      }

      public void Dispose()
      {
         container = null;
      }
   }
}