// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertySelectionStrategies.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies
{
    /// <summary>Predefined strategies for selecting the properties to inject</summary>
    public class PropertySelectionStrategies
    {
        #region Constants and Fields

        private static readonly AttributePropertySelectionStrategy allWithAttribute = new AttributePropertySelectionStrategy(true);

        private static readonly AttributePropertySelectionStrategy publicWithAttribute = new AttributePropertySelectionStrategy(false);

        #endregion Constants and Fields

        #region Public Properties

        /// <summary>Gets all properties decorated with the <see cref="DependencyAttribute"/>.</summary>
        public static AttributePropertySelectionStrategy AllWithDepencencyAttribute
        {
            get
            {
                return allWithAttribute;
            }
        }

        /// <summary>Gets the public properties decorated with the <see cref="DependencyAttribute"/>.</summary>
        public static AttributePropertySelectionStrategy OnlyPublicWithDepencencyAttribute
        {
            get
            {
                return publicWithAttribute;
            }
        }

        #endregion Public Properties
    }
}