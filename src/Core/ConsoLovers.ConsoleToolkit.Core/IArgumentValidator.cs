// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentValidator.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Reflection;

   using JetBrains.Annotations;

   // TODO Add more build in validators NotNullOrEmpty, IsInRange...
   // TODO Support Attribute derived types e.g. [NotNullOrWhitespace] as attribute

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

   public interface IValidationContext
   {
      #region Public Properties

      /// <summary>Gets the name of the argument that should be validated.</summary>
      string ArgumentName { get; }

      /// <summary>Gets the <see cref="PropertyInfo"/> for the property that is validated.</summary>
      PropertyInfo Property { get; }

      /// <summary>Gets the command line attribute of the argument.</summary>
      CommandLineAttribute CommandLineAttribute { get; }

      #endregion
   }

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
}