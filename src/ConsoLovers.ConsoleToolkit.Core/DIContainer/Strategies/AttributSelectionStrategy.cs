// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributSelectionStrategy.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary><see cref="ConstructorSelectionStrategy"/> that finds the first constructor decoreated with the <see cref="InjectionConstructorAttribute"/></summary>
    public class AttributSelectionStrategy : ConstructorSelectionStrategy
    {
        #region Private Methods

        private bool HasInjectionConstructorAttribute(ConstructorInfo constructor)
        {
            return constructor.GetCustomAttributes(typeof(InjectionConstructorAttribute), true).Length > 0;
        }

        private int ParametersCount(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Length;
        }

        #endregion Private Methods

        #region Internal Constructors

        /// <summary>Initializes a new instance of the <see cref="AttributSelectionStrategy"/> class.</summary>
        internal AttributSelectionStrategy()
        {
        }

        #endregion Internal Constructors

        #region Internal Methods

        /// <summary>
        ///    Selects the first costructor decorated with the <see cref="InjectionConstructorAttribute"/>. If <paramref name="maximalParameters"/> is set to true and more than one
        ///    constructor has the <see cref="InjectionConstructorAttribute"/>, the one with the most parameters ist selected.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="maximalParameters">if set to <c>true</c> the one with the most parameters ist selected.</param>
        /// <returns>The selected <see cref="ConstructorInfo"/> or null of no constructor matched the stategies selection conditions.</returns>
        internal ConstructorInfo SelectCostructor(Type type, bool maximalParameters)
        {
            if (maximalParameters)
            {
                return type.GetConstructors().Where(HasInjectionConstructorAttribute).OrderByDescending(ParametersCount).FirstOrDefault();
            }
            else
            {
                return type.GetConstructors().Where(HasInjectionConstructorAttribute).OrderBy(ParametersCount).FirstOrDefault();
            }
        }

        #endregion Internal Methods

        #region Public Methods

        /// <summary>Selects the first costructor decorated with the <see cref="InjectionConstructorAttribute"/>.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The selected <see cref="ConstructorInfo"/> or null of no constructor matched the stategies selection conditions.</returns>
        public override ConstructorInfo SelectCostructor(Type type)
        {
            return SelectCostructor(type, false);
        }

        #endregion Public Methods
    }
}