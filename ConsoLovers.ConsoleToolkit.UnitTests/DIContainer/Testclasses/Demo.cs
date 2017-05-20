// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Demo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.DIContainer.Testclasses
{
   /// <summary>Testclass for container tests</summary>
   internal class Demo : IDemo
   {
      #region Constants and Fields

      private readonly int id = 1;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="Demo"/> class.</summary>
      public Demo()
      {
      }

      /// <summary>Initializes a new instance of the <see cref="Demo"/> class.</summary>
      /// <param name="id">The id.</param>
      public Demo(int id)
      {
         this.id = id;
      }

      #endregion

      #region IDemo Members

      /// <summary>Gets or sets the name.</summary>
      public string Name { get; set; }

      /// <summary>Gets the id.</summary>
      /// <returns>the id</returns>
      public int GetId()
      {
         return id;
      }

      #endregion
   }
}