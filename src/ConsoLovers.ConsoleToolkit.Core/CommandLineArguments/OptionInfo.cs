// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ParameterInfo" />
    public class OptionInfo : ParameterInfo
    {
        #region Public Constructors

        public OptionInfo([NotNull] PropertyInfo propertyInfo, [NotNull] OptionAttribute commandLineAttribute)
         : base(propertyInfo, commandLineAttribute)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public OptionAttribute Attribute => (OptionAttribute)CommandLineAttribute;

        #endregion Public Properties
    }
}