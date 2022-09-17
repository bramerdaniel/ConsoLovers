// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateNotNullOrWhitespaceAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

public class ValidateNotNullOrWhitespaceAttribute : ArgumentValidatorAttribute, IArgumentValidator<string>
{
   #region Constructors and Destructors

   public ValidateNotNullOrWhitespaceAttribute()
      : base(typeof(ValidateNotNullOrWhitespaceAttribute))
   {
   }

   #endregion

   #region IArgumentValidator<string> Members

   public void Validate(IValidationContext context, string value)
   {
      if (value == null)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must not be null.");

      if (string.IsNullOrWhiteSpace(value))
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must not be empty.");
   }

   #endregion
}