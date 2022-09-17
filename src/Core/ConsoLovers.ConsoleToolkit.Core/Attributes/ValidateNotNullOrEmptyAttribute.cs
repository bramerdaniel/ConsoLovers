// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateNotNullOrEmptyAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

public class ValidateNotNullOrEmptyAttribute : ArgumentValidatorAttribute, IArgumentValidator<string>
{
   #region Constructors and Destructors

   public ValidateNotNullOrEmptyAttribute()
      : base(typeof(ValidateNotNullOrEmptyAttribute))
   {
   }

   #endregion

   #region IArgumentValidator<string> Members

   public void Validate(IValidationContext context, string value)
   {
      if (value == null)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must not be null");
      if (string.IsNullOrEmpty(value))
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must not be empty");
   }

   #endregion
}