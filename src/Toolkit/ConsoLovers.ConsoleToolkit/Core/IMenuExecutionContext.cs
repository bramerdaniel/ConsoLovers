// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuExecutionContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IMenuExecutionContext
   {
      // TODO Change the name of the ICustomHeader interface to something better (e.g. IMenuHeader)

      ConsoleMenuItem MenuItem { get; }
   }

   internal struct MenuExecutionContext : IMenuExecutionContext
   {
      public ConsoleMenuItem MenuItem { get; set; }
   }
}