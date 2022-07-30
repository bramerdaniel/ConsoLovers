// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationTestContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;
   using System.Linq;
   using System.Threading;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using Moq;

   internal class ApplicationTestContext<T> : IDisposable
      where T : class
   {
      #region Constants and Fields

      private Container container;

      #endregion

      #region Constructors and Destructors

      public ApplicationTestContext()
      {
         container = new Container();

         Commands = new Mock<ICommandVerification>();
         Application = new Mock<IApplicationVerification<T>>();

         container.Register<ICommandVerification>(Commands.Object);
         container.Register<IApplicationVerification<T>>(Application.Object);
         Factory = new DefaultFactory(container);
      }

      #endregion

      #region IDisposable Members

      public void Dispose()
      {
         container = null;
      }

      #endregion

      #region Public Properties

      public Mock<IApplicationVerification<T>> Application { get; }

      public Mock<ICommandVerification> Commands { get; }

      public DefaultFactory Factory { get; }

      #endregion

      #region Public Methods and Operators

      public void RunApplication(string args)
      {
         ConsoleApplicationManager.For<TestApplication<T>>().UsingFactory(Factory).Run(args);
      }

      public void RunApplicationAsync(string args)
      {
         ConsoleApplicationManager.For<TestApplication<T>>()
            .UsingFactory(Factory)
            .RunAsync(args, CancellationToken.None)
            .GetAwaiter().GetResult();
      }

      public void RunApplication(params string[] args)
      {
         RunApplication(string.Join(" ", args));
      }

      #endregion

      public void VerifyCommandExecuted<TC>()
       where TC : ICommand
      {
         Application.Verify(a => a.RunWithCommand(It.IsAny<TC>()), Times.Once);
      }
   }
}