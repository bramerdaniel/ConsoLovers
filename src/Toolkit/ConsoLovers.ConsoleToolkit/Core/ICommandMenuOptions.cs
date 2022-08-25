// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandMenuOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface ICommandMenuOptions
   {
      #region Public Properties

      IConsoleMenuOptions Menu { get; }

      MenuBuilderBehaviour MenuBehaviour { get; set; }

      ArgumentInitializationModes DefaultArgumentInitializationMode { get; set; }

      #endregion
   }
}