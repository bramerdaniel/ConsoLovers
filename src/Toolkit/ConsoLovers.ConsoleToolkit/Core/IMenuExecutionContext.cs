// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuExecutionContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IMenuExecutionContext
   {
      #region Public Properties

      /// <summary>Gets the menu item that triggered the execution of the command.</summary>
      ConsoleMenuItem MenuItem { get; }

      #endregion

      #region Public Methods and Operators


      #endregion
   }
}