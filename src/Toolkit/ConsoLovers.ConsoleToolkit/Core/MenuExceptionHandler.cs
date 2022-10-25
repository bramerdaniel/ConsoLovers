// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuExceptionHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.Input;

   using JetBrains.Annotations;

   /// <summary>NotSpecified implementation of the <see cref="IMenuExceptionHandler"/></summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IMenuExceptionHandler"/>
   public class MenuExceptionHandler : IMenuExceptionHandler
   {
      #region Constants and Fields

      private readonly IConsole console;

      private readonly ICommandMenuOptions menuBuilderOptions;

      #endregion

      #region Constructors and Destructors

      public MenuExceptionHandler([NotNull] IConsole console, [NotNull] ICommandMenuOptions menuBuilderOptions)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
         this.menuBuilderOptions = menuBuilderOptions ?? throw new ArgumentNullException(nameof(menuBuilderOptions));
      }

      #endregion

      #region IMenuExceptionHandler Members

      public bool Handle(Exception exception)
      {
         if (exception is InputCanceledException && menuBuilderOptions.BuilderOptions.ArgumentInitializationCancellation == InitializationCancellationMode.CancelSilent)
            return true;

         console.WriteLine(exception.Message, ConsoleColor.Red);
         
         // When an error occurred during the execution of a menu item,
         // the user must be able to read it, that's why we need to wait here
         console.ReadLine();

         return true;
      }

      #endregion
   }
}