// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuOptions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Menu;

   internal class CommandMenuOptions : ICommandMenuOptions
   {
      #region Constructors and Destructors

      public CommandMenuOptions()
      {
         Menu = new ConsoleMenuOptions();
      }

      #endregion

      #region ICommandMenuOptions Members

      public IConsoleMenuOptions Menu { get; }

      public MenuBuilderBehaviour MenuBehaviour { get; set; }

      #endregion
   }
}