// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>Data class containing all information for a defined command line parameter in its according data class</summary>
   [DebuggerDisplay("[{" + nameof(ParameterName) + "}]")]
   public abstract class ParameterInfo
   {
      #region Constructors and Destructors

      protected ParameterInfo([NotNull] PropertyInfo propertyInfo, [NotNull] CommandLineAttribute commandLineAttribute)
      {
         if (propertyInfo == null)
            throw new ArgumentNullException(nameof(propertyInfo));
         if (commandLineAttribute == null)
            throw new ArgumentNullException(nameof(commandLineAttribute));

         PropertyInfo = propertyInfo;
         ParameterType = propertyInfo.PropertyType;
         CommandLineAttribute = commandLineAttribute;
         Identifiers = commandLineAttribute.GetIdentifiers().ToArray();
         ParameterName = commandLineAttribute.Name ?? PropertyInfo.Name;

      }

      #endregion

      #region Public Properties

      /// <summary>Gets the defined names.</summary>
      public string[] Identifiers { get; }

      public string ParameterName { get; }

      /// <summary>Gets the type of the property that was decorated with the <see cref="CommandLineAttribute"/>.</summary>
      public Type ParameterType { get; }

      /// <summary>Gets the <see cref="PropertyInfo"/> of the property that was decorated with the <see cref="CommandLineAttribute"/>.</summary>
      public PropertyInfo PropertyInfo { get; }

      #endregion

      #region Properties

      protected CommandLineAttribute CommandLineAttribute { get; }

      #endregion
   }
}