// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuExecutionContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IMenuExecutionContext
   {
      #region Public Properties

      // TODO Change the name of the ICustomHeader interface to something better (e.g. IMenuHeader)

      ConsoleMenuItem MenuItem { get; }

      #endregion
   }

   internal struct MenuExecutionContext : IMenuExecutionContext
   {
      #region IMenuExecutionContext Members

      public ConsoleMenuItem MenuItem { get; set; }

      #endregion
   }
}