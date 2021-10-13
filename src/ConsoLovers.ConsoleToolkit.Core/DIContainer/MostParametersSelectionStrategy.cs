// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MostParametersSelectionStrategy.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
    #region

    using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;
    using System;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary><see cref="ConstructorSelectionStrategy"/> that finds the first constructor with the most parameters</summary>
    public class MostParametersSelectionStrategy : ConstructorSelectionStrategy
    {
        #region Constructors and Destructors

        #region Internal Constructors

        /// <summary>Initializes a new instance of the <see cref="MostParametersSelectionStrategy"/> class.</summary>
        internal MostParametersSelectionStrategy()
        {
        }

        #endregion Internal Constructors

        #endregion

        #region Public Methods and Operators

        #region Public Methods

        /// <summary>Selects the constructor.</summary>
        /// <param name="type">The type. </param>
        /// <returns>The selected <see cref="ConstructorInfo"/> or null of no constructor matched the strategies selection conditions. </returns>
        public override ConstructorInfo SelectCostructor(Type type)
        {
            return type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
        }

        #endregion Public Methods

        #endregion
    }
}