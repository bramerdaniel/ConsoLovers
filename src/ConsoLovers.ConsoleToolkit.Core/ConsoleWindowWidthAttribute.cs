// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleWindowWidthAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
    using System;

    /// <summary>Attribute that is used to set the window width of the console window</summary>
    /// <seealso cref="System.Attribute"/>
    public class ConsoleWindowWidthAttribute : Attribute
    {
        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="ConsoleWindowTitleAttribute"/> class.</summary>
        /// <param name="width">The width.</param>
        public ConsoleWindowWidthAttribute(int width)
        {
            ConsoleWidth = width;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets or sets a value indicating whether shrinking of the width is allowed.</summary>
        public bool AllowShrink { get; set; } = true;

        /// <summary>Gets the width of the console window.</summary>
        public int ConsoleWidth { get; }

        #endregion Public Properties
    }
}