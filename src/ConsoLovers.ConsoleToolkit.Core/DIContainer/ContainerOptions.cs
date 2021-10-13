// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
    using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;

    /// <summary>Options class for the <see cref="Container"/>.</summary>
    public class ContainerOptions
    {
        #region Private Fields

        private ConstructorSelectionStrategy constructorSelectionStrategy;

        private PropertySelectionStrategy propertySelectionStrategy;

        #endregion Private Fields

        #region Public Properties

        /// <summary>Gets or sets the <see cref="ConstructorSelectionStrategy"/>.</summary>
        public ConstructorSelectionStrategy ConstructorSelectionStrategy
        {
            get
            {
                if (constructorSelectionStrategy == null)
                    constructorSelectionStrategy = new CombinedSelectionStrategy();
                return constructorSelectionStrategy;
            }

            set
            {
                constructorSelectionStrategy = value;
            }
        }

        /// <summary>Gets or sets the <see cref="ConstructorSelectionStrategy"/>.</summary>
        public PropertySelectionStrategy PropertySelectionStrategy
        {
            get
            {
                if (propertySelectionStrategy == null)
                    propertySelectionStrategy = new AttributePropertySelectionStrategy(false);
                return propertySelectionStrategy;
            }

            set
            {
                propertySelectionStrategy = value;
            }
        }

        #endregion Public Properties
    }
}