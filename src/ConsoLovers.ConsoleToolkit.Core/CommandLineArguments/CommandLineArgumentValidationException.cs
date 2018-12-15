// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentValidationException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Runtime.Serialization;

   /// <summary>Exception that is thrown when the validation of a command line argument failed</summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.CommandLineArgumentException"/>
   public class CommandLineArgumentValidationException : CommandLineArgumentException
   {
      #region Constructors and Destructors

      public CommandLineArgumentValidationException()
      {
      }

      public CommandLineArgumentValidationException(string message)
         : base(message)
      {
      }

      public CommandLineArgumentValidationException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      protected CommandLineArgumentValidationException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }

      #endregion
   }
}