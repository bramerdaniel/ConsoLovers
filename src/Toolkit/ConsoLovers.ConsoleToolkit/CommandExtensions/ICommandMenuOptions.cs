// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandMenuOptions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface ICommandMenuOptions
   {
      IConsoleMenuOptions Menu { get; }

      MenuBuilderBehaviour MenuBehaviour { get; set; }
   }
}