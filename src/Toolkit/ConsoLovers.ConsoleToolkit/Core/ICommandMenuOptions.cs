// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandMenuOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface ICommandMenuOptions
   {
      #region Public Properties

      IConsoleMenuOptions MenuOptions { get; }

      IMenuBuilderOptions BuilderOptions { get; }

      #endregion
   }
}