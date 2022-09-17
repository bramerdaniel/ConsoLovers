// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Reflection;

using JetBrains.Annotations;

internal class ValidationContext : IValidationContext
{
   #region Constructors and Destructors

   public ValidationContext([NotNull] string argumentName, [NotNull] PropertyInfo property, [NotNull] CommandLineAttribute commandLineAttribute)
   {
      ArgumentName = argumentName ?? throw new ArgumentNullException(nameof(argumentName));
      Property = property ?? throw new ArgumentNullException(nameof(property));
      CommandLineAttribute = commandLineAttribute ?? throw new ArgumentNullException(nameof(commandLineAttribute));
   }

   #endregion

   #region IValidationContext Members

   public string ArgumentName { get; }

   public PropertyInfo Property { get; }

   public CommandLineAttribute CommandLineAttribute { get; }

   #endregion
}