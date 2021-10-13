// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelpRequest.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;
    using System.Reflection;

    public class PropertyHelpRequest
    {
        #region Public Constructors

        public PropertyHelpRequest([NotNull] PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));

            CommandLineAttribute = Property.GetAttribute<CommandLineAttribute>();
            DetailedHelpTextAttribute = Property.GetAttribute<DetailedHelpTextAttribute>();
            HelpTextAttribute = Property.GetAttribute<HelpTextAttribute>();
        }

        #endregion Public Constructors

        #region Public Properties

        public CommandLineAttribute CommandLineAttribute { get; }

        public DetailedHelpTextAttribute DetailedHelpTextAttribute { get; }

        public HelpTextAttribute HelpTextAttribute { get; }

        /// <summary>Gets the property.</summary>
        public PropertyInfo Property { get; }

        #endregion Public Properties
    }
}