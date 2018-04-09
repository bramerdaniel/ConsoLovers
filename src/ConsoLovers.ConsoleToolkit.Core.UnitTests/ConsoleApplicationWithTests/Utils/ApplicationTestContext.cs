// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationTestContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using Moq;

   internal class ApplicationTestContext<T> : IDisposable
      where T : class, IApplication
   {
      #region Constants and Fields

      private Container container;

      #endregion

      #region Constructors and Destructors

      public ApplicationTestContext()
      {
         container = new Container();

         Commands = new Mock<ICommandVerification>();
         Application = new Mock<IApplicationVerification>();

         container.Register<ICommandVerification>(Commands.Object);
         container.Register<IApplicationVerification>(Application.Object);
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

      public Mock<IApplicationVerification> Application { get; }

      public Mock<ICommandVerification> Commands { get; }

      public DefaultFactory Factory { get; }

      #endregion

      #region Public Methods and Operators

      public void RunApplication(params string[] args)
      {
         ConsoleApplicationManager.For<T>().UsingFactory(Factory).Run(args);
      }

      #endregion
   }
}