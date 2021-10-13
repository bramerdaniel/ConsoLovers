// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;

    public class TypeHelpRequest
    {
        #region Public Constructors

        public TypeHelpRequest([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets the type the request was created for.</summary>
        public Type Type { get; }

        #endregion Public Properties
    }
}