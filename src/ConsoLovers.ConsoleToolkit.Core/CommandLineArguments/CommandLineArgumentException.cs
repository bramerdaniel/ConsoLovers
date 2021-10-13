// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>Represents errors that occur during command line parsing.</summary>
    public class CommandLineArgumentException : Exception
    {
        #region Protected Constructors

        /// <summary>Initializes a new instance of the <see cref="CommandLineArgumentException"/> class.</summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected CommandLineArgumentException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

        #endregion Protected Constructors

        #region Internal Properties

        internal ErrorReason Reason { get; set; }

        #endregion Internal Properties

        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="CommandLineArgumentException"/> class.</summary>
        public CommandLineArgumentException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CommandLineArgumentException"/> class.</summary>
        /// <param name="message">The message.</param>
        public CommandLineArgumentException(string message)
           : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CommandLineArgumentException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CommandLineArgumentException(string message, Exception innerException)
           : base(message, innerException)
        {
        }

        #endregion Public Constructors
    }
}