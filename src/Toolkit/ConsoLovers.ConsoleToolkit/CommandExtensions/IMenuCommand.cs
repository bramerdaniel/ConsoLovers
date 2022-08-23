// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   public interface IMenuCommand
   {
      #region Public Methods and Operators

      /// <summary>The executes method is called when the command was executed from the menu.</summary>
      /// <param name="context">The context providing information about the calling menu item.</param>
      void Execute(IMenuExecutionContext context);

      #endregion
   }
}