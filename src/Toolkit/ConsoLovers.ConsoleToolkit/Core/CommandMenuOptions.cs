// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
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