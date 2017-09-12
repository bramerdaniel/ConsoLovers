// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectWithNamedDependencies.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.DIContainer.Testclasses
{
   using ConsoLovers.ConsoleToolkit.DIContainer;

   /// <summary>Testclass for container tests</summary>
   public class ObjectWithNamedDependencies
   {
      #region Public Properties

      /// <summary>Gets or sets the demo.</summary>
      [Dependency(Name = "Name")]
      public IDemo Demo { get; set; }

      #endregion
   }
}