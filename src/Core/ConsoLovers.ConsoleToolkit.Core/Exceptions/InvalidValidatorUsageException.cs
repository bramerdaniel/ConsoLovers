// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidValidatorUsageException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Exceptions
{
   using System;
   using System.Runtime.Serialization;

   /// <summary>Occurs when an <see cref="IArgumentValidator{T}"/> was not implemented correctly</summary>
   /// <seealso cref="CommandLineArgumentException"/>
   public class InvalidValidatorUsageException : CommandLineArgumentException
   {
      #region Constructors and Destructors

      public InvalidValidatorUsageException()
      {
      }

      public InvalidValidatorUsageException(string message)
         : base(message)
      {
      }

      public InvalidValidatorUsageException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      protected InvalidValidatorUsageException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }

      #endregion
   }
}