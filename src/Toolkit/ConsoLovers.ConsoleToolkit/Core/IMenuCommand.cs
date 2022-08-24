// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   // TODO Add IAsyncMenuCommand

   public interface IMenuCommand
   {
      #region Public Methods and Operators

      /// <summary>The executes method is called when the command was executed from the menu.</summary>
      /// <param name="context">The context providing information about the calling menu item.</param>
      void Execute(IMenuExecutionContext context);

      #endregion
   }
}