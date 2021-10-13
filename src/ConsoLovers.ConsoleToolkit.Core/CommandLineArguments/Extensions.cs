// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class Extensions
    {
        #region Public Methods

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttributes<T>(true).FirstOrDefault();
        }

        public static int GetIndex(this CommandLineAttribute attribute)
        {
            return attribute is ArgumentAttribute argumentAttribute ? argumentAttribute.Index : -1;
        }

        public static bool IsCommandType(this Type type)
        {
            var command = type.GetInterface(typeof(ICommand).FullName);
            if (command != null)
                return true;

            command = type.GetInterface(typeof(ICommand<>).FullName);
            return command != null;
        }

        public static bool IsRequired(this CommandLineAttribute attribute)
        {
            return attribute is ArgumentAttribute argumentAttribute && argumentAttribute.Required;
        }

        public static bool TrimQuotation(this CommandLineAttribute attribute)
        {
            return attribute is ArgumentAttribute argumentAttribute && argumentAttribute.TrimQuotation;
        }

        #endregion Public Methods
    }
}