// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentHelp.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System.Text;

    /// <summary>Contains information for a command line argument, that can be used for printing a help</summary>
    public class ArgumentHelp
    {
        #region Constants and Fields

        private string aliasString;

        #endregion Constants and Fields

        #region Public Properties

        /// <summary>Gets or sets the aliases that can be used for setting the argument.</summary>
        public string[] Aliases { get; set; }

        /// <summary>Gets the aliases as comma separated string.</summary>
        public string AliasString => aliasString ?? (aliasString = ToCommaSeperatedString());

        /// <summary>Gets the <see cref="LocalizedDescription"/> if available, otherwise the <see cref="UnlocalizedDescription"/> is returned.</summary>
        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(LocalizedDescription))
                    return LocalizedDescription;

                return string.IsNullOrEmpty(UnlocalizedDescription) ? "No help available" : UnlocalizedDescription;
            }
        }

        /// <summary>Gets or sets the localized description if it is available.</summary>
        public string LocalizedDescription { get; set; }

        /// <summary>Gets or sets the order.</summary>
        public int Priority { get; set; }

        /// <summary>Gets or sets the name of the property the argument was mapped to.</summary>
        public string PropertyName { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="ArgumentHelp"/> is required.</summary>
        public bool Required { get; set; }

        /// <summary>Gets or sets the un localized description specified by the developer.</summary>
        public string UnlocalizedDescription { get; set; }

        #endregion Public Properties

        #region Methods

        private string ToCommaSeperatedString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var alias in Aliases)
                builder.AppendFormat("{0}, ", alias);

            return builder.ToString().TrimEnd(',', ' ');
        }

        #endregion Methods
    }
}