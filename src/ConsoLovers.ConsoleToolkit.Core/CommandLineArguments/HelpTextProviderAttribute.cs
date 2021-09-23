// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpTextProviderAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;
    using System.Xml.Serialization;

    /// <summary>Base attribute class for the usage with the <see cref="ArgumentMapper{T}"/> class.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HelpTextProviderAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="CommandLineAttribute"/> class.</summary>
        public HelpTextProviderAttribute([NotNull] Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            Type = type;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>Gets the name of the argument.</summary>
        [XmlIgnore]
        public Type Type { get; set; }

        #endregion Public Properties
    }
}