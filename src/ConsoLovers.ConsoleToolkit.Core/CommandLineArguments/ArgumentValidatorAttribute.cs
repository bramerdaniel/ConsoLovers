// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentValidatorAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;

    /// <summary>Attribute that is used to specify the type of an <see cref="IArgumentValidator{T}"/> implementation.</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ArgumentValidatorAttribute : Attribute
    {
        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="CommandLineAttribute"/> class.</summary>
        /// <param name="type">The name.</param>
        public ArgumentValidatorAttribute([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets the type of an <see cref="IArgumentValidator{T}"/> implementation.</summary>
        public Type Type { get; }

        #endregion Public Properties
    }
}