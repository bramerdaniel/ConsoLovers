// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentValidator.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   /// <summary>Interface for implementing custom command line argument validation</summary>
   /// <typeparam name="T">The type of the value that should be validated</typeparam>
   public interface IArgumentValidator<in T>
   {
      #region Public Methods and Operators

      /// <summary>Validates the specified value.</summary>
      /// <param name="context">The context containing the information about the argument that has to be validates.</param>
      /// <param name="value">The value that should be validated.</param>
      void Validate(IValidationContext context, T value);

      #endregion
   }
}