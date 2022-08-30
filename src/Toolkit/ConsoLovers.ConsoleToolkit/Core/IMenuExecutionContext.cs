// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuExecutionContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   public interface IMenuExecutionContext
   {
      #region Public Properties

      /// <summary>Gets the menu item that triggered the execution of the command.</summary>
      ConsoleMenuItem MenuItem { get; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Creates the arguments instance for the current command.</summary>
      object GetOrCreateArguments();

      /// <summary>Initializes the argument with the specified name.</summary>
      /// <param name="argumentName">Name of the argument to initialize.</param>
      object InitializeArgument(string argumentName);

      /// <summary>Initializes all visible arguments.</summary>
      void InitializeArguments();
      
      #endregion
   }
}