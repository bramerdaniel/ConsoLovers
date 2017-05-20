// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoAttributes.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.DIContainer.Testclasses
{
   /// <summary>Testclass with no attributes on its methods, properties, and constructors</summary>
   internal class NoAttributes
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="NoAttributes"/> class.</summary>
      public NoAttributes()
      {
         Id = 1;
      }

      /// <summary>Initializes a new instance of the <see cref="NoAttributes"/> class.</summary>
      /// <param name="id">The name. </param>
      public NoAttributes(int id)
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