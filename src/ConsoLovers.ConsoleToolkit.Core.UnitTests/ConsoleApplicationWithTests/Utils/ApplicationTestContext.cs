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

   using Microsoft.Extensions.DependencyInjection;

   using Moq;

   internal class ApplicationTestContext<T> : IDisposable
      where T : class
   {
      #region Constructors and Destructors

      public ApplicationTestContext()
      {
         Commands = new Mock<ICommandVerification>();
         Application = new Mock<IApplicationVerification<T>>();
      }

      #endregion

      #region IDisposable Members

      public void Dispose()
      {
      }

      #endregion

      #region Public Properties

      public Mock<IApplicationVerification<T>> Application { get; }

      public Mock<ICommandVerification> Commands { get; }
      

      #endregion

      #region Public Methods and Operators

      public void RunApplication(string args)
      {
         ConsoleApplicationManager.For<TestApplication<T>>()
            .ConfigureServices(services =>
            {
               services
                  .AddSingleton(Commands.Object)
                  .AddSingleton(Application.Object);
            })
            .Run(args);
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