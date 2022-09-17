// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllowedRange.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

public class AllowedRange : ArgumentValidatorAttribute, IArgumentValidator<int>
{
   #region Constructors and Destructors

   public AllowedRange()
      : base(typeof(AllowedRange))
   {
   }

   #endregion

   #region IArgumentValidator<int> Members

   public void Validate(IValidationContext context, int value)
   {
      if (value < Min)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must be have at least a value of {Min}.");

      if (value > Max)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} has a defined maximum of {Max}.");
   }

   #endregion

   #region Public Properties

   public int Max { get; set; } = int.MaxValue;

   public int Min { get; set; } = int.MinValue;

   #endregion

   #region Public Methods and Operators

   public void Validate(IValidationContext context, string value)
   {
      if (value == null)
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must not be null.");

      if (string.IsNullOrWhiteSpace(value))
         throw new CommandLineArgumentValidationException($"Argument {context.ArgumentName} must not be empty.");
   }

   #endregion
}