// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotNullOrWhitespaceAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using ConsoLovers.ConsoleToolkit.Core.Exceptions;

using JetBrains.Annotations;

public class NotNullOrWhitespaceAttribute : ArgumentValidatorAttribute, IArgumentValidator<string>
{
   public NotNullOrWhitespaceAttribute()
      : base(typeof(NotNullOrWhitespaceAttribute))
   {
   }

   public void Validate(string value)
   {
      if (value == null)
         throw new CommandLineArgumentValidationException("Argument must not be null");
      if(string.IsNullOrWhiteSpace(value))
         throw new CommandLineArgumentValidationException("Argument must not be empty");
   }
}