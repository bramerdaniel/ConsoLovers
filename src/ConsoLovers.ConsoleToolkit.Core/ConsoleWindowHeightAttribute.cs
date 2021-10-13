// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleWindowHeightAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
    using System;

    /// <summary>Attribute that is used to set the window height of the console window</summary>
    /// <seealso cref="System.Attribute"/>
    public class ConsoleWindowHeightAttribute : Attribute
    {
        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="ConsoleWindowTitleAttribute"/> class.</summary>
        /// <param name="height">The width.</param>
        public ConsoleWindowHeightAttribute(int height)
        {
            ConsoleHeight = height;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets or sets a value indicating whether shrinking of the height is allowed.</summary>
        public bool AllowShrink { get; set; } = true;

        /// <summary>Gets the height of the console window.</summary>
        public int ConsoleHeight { get; }

        #endregion Public Properties
    }
}