// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateIsInRangeAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

/// <summary><see cref="IArgumentValidator{T}"/> for a specified integer range</summary>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.ArgumentValidatorAttribute"/>
public class ValidateIsInRangeAttribute : ArgumentValidatorAttribute, IArgumentValidator<int>
{
   #region Constructors and Destructors

   public ValidateIsInRangeAttribute()
      : base(typeof(ValidateIsInRangeAttribute))
   {
   }

   #endregion

   #region IArgumentValidator<int> Members

   public void Validate(IValidationContext context, int value)
   {
      if (value < Min)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} has a defined minimum of {Min}.");

      if (value > Max)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} has a defined maximum of {Max}.");
   }

   #endregion

   #region Public Properties

   /// <summary>Determines the maximum value of the parameters.</summary>
   public int Max { get; set; } = int.MaxValue;

   /// <summary>Determines the minimum value of the parameters.</summary>
   public int Min { get; set; } = int.MinValue;

   #endregion
}