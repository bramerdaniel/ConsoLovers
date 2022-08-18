// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OneAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses
{
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;

   /// <summary>Test class for <see cref="ConstructorSelectionStrategy"/></summary>
   internal class OneAttribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="OneAttribute"/> class.</summary>
      [InjectionConstructor]
      public OneAttribute()
      {
         Id = 1;
      }

      /// <summary>Initializes a new instance of the <see cref="OneAttribute"/> class.</summary>
      /// <param name="id">The name. </param>
      public OneAttribute(int id)
      {
         Id = id;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the id.</summary>
      public int Id { get; private set; }

      #endregion
   }
}