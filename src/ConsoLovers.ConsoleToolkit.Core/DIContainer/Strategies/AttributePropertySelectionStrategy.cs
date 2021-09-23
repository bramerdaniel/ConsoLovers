// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributePropertySelectionStrategy.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>Strategy that selects the properties, decorated with the <see cref="DependencyAttribute"/></summary>
    public class AttributePropertySelectionStrategy : PropertySelectionStrategy
    {
        #region Constants and Fields

        private readonly bool injectProtectedAndPrivate;

        #endregion Constants and Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AttributePropertySelectionStrategy"/> class.</summary>
        /// <param name="injectProtectedAndPrivate">if set to <c>true</c> [inject protected and private].</param>
        public AttributePropertySelectionStrategy(bool injectProtectedAndPrivate)
        {
            this.injectProtectedAndPrivate = injectProtectedAndPrivate;
        }

        #endregion Constructors and Destructors

        #region Public Methods and Operators

        /// <summary>Selects all properties of the given type.</summary>
        /// <param name="type">The type to select the properties from.</param>
        /// <returns>The <see cref="PropertyInfo"/>s that were selected</returns>
        public override IEnumerable<PropertyInfo> SelectProperties(Type type)
        {
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (property.HasAttribute(typeof(DependencyAttribute)))
                {
                    if (injectProtectedAndPrivate)
                    {
                        if (property.CanRead && property.CanWrite)
                            yield return property;
                    }
                    else
                    {
                        if (property.CanRead && property.CanWrite && property.PropertyType.IsInterface && property.HasPublicSetter())
                            yield return property;
                    }
                }
            }
        }

        #endregion Public Methods and Operators
    }
}