// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Menu;

   internal class CommandMenuOptions : ICommandMenuOptions
   {
      #region Constructors and Destructors

      public CommandMenuOptions()
      {
         MenuOptions = new ConsoleMenuOptions();
         BuilderOptions = new MenuBuilderOptions();
      }

      #endregion

      #region ICommandMenuOptions Members

      public IConsoleMenuOptions MenuOptions { get; }

      public IMenuBuilderOptions BuilderOptions { get; }

      #endregion
   }
}