// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   public class GenericCommand<T> : ICommand<T>
   {
      #region ICommand Members

      public virtual void Execute()
      {
      }

      #endregion

      #region ICommand<T> Members

      public T Arguments { get; set; }

      #endregion
   }
}