// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineAttributeException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Reflection;
   using System.Runtime.Serialization;

   /// <summary>Exception that is thrown when an <see cref="CommandLineAttribute"/> us used in an invalid way.</summary>
   [Serializable]
   public class CommandLineAttributeException : CommandLineArgumentException
   {
      #region Constructors and Destructors

      /// <summary>
      ///    Initializes a new instance of the <see cref="CommandLineAttributeException"/> class. Initializes a new instance of the <see cref="T:System.Exception"/> class with a
      ///    specified error message.
      /// </summary>
      /// <param name="message">The message that describes the error.</param>
      public CommandLineAttributeException(string message)
         : base(message)
      {
      }

      public CommandLineAttributeException()
      {
      }

      public CommandLineAttributeException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      protected CommandLineAttributeException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the first property that defines the name as name or alias.</summary>
      public PropertyInfo FirstProperty { get; internal set; }

      /// <summary>Gets the name of the command line argument that is not unique.</summary>
      public string Name { get; internal set; }

      /// <summary>Gets the second property that defines the name as name or alias.</summary>
      public PropertyInfo SecondProperty { get; internal set; }

      #endregion
   }
}