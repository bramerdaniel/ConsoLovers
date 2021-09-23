// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CombinedSelectionStrategy.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies
{
    #region

    using System;
    using System.Reflection;

    #endregion

    /// <summary><see cref="ConstructorSelectionStrategy"/> that finds the first constructor decoreated with the <see cref="InjectionConstructorAttribute"/></summary>
    public class CombinedSelectionStrategy : MostParametersSelectionStrategy
    {
        #region Constants and Fields

        private readonly AttributSelectionStrategy defaultSelection = new AttributSelectionStrategy();

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="CombinedSelectionStrategy"/> class.</summary>
        internal CombinedSelectionStrategy()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///    Selects the first costructor that has a <see cref="InjectionConstructorAttribute"/> . If no constructor has a <see cref="InjectionConstructorAttribute"/> the one with the
        ///    most parameters is selected.
        /// </summary>
        /// <param name="type">The type. </param>
        /// <returns>The selected <see cref="ConstructorInfo"/> or null of no constructor matched the stategies selection conditions. </returns>
        public override ConstructorInfo SelectCostructor(Type type)
        {
            var costructor = defaultSelection.SelectCostructor(type, true);
            if (costructor == null)
                costructor = base.SelectCostructor(type);
            return costructor;
        }

        #endregion
    }
}