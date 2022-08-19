// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuExecutionContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IMenuExecutionContext
   {
      ConsoleMenuItem MenuItem { get; }
   }

   internal struct MenuExecutionContext : IMenuExecutionContext
   {
      public ConsoleMenuItem MenuItem { get; set; }
   }
}