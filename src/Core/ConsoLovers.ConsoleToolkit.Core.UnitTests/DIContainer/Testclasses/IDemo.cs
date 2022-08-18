// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDemo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses
{
   /// <summary>test interface for container tests</summary>
   public interface IDemo
   {
      #region Public Properties

      /// <summary>Gets or sets the name.</summary>
      string Name { get; set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Gets the id.</summary>
      /// <returns>the id</returns>
      int GetId();

      #endregion
   }
}