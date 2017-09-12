// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Contracts;

   public abstract class ConsoleApplication : IConsoleApplication
   {
      #region IConsoleApplication Members

      public virtual void Initialize(string[] args)
      {
      }

      public int Exit()
      {
         return 0;
      }

      #endregion

      #region IApplication Members

      public abstract void Run();

      #endregion

      #region Public Properties

      public static IConsole Console { get; } = new ConsoleProxy();

      #endregion
   }
}