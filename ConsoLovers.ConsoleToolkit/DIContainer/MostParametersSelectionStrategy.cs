// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MostParametersSelectionStrategy.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   #region

   using System;
   using System.Linq;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.DIContainer.Strategies;

   #endregion

   /// <summary><see cref="ConstructorSelectionStrategy"/> that finds the first constructor with the most parameters</summary>
   public class MostParametersSelectionStrategy : ConstructorSelectionStrategy
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="MostParametersSelectionStrategy"/> class.</summary>
      internal MostParametersSelectionStrategy()
      {
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Selects the constructor.</summary>
      /// <param name="type">The type. </param>
      /// <returns>The selected <see cref="ConstructorInfo"/> or null of no constructor matched the strategies selection conditions. </returns>
      public override ConstructorInfo SelectCostructor(Type type)
      {
         return type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
      }

      #endregion
   }
}