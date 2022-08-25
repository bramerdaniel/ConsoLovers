// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuExceptionHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using JetBrains.Annotations;

   /// <summary>Default implementation of the <see cref="IMenuExceptionHandler"/></summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IMenuExceptionHandler"/>
   public class MenuExceptionHandler : IMenuExceptionHandler
   {
      #region Constants and Fields

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public MenuExceptionHandler([NotNull] IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      #endregion

      #region IMenuExceptionHandler Members

      public bool Handle(Exception exception)
      {
         console.WriteLine(exception.Message, ConsoleColor.Red);

         // When an error occurred during the execution of a menu item,
         // the user must be able to read it, that's why we need to wait here
         console.ReadLine();

         return true;
      }

      #endregion
   }
}