// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidValidatorUsageException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>Occurs when an <see cref="IArgumentValidator{T}"/> was not implemented correctly</summary>
    /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.CommandLineArgumentException"/>
    public class InvalidValidatorUsageException : CommandLineArgumentException
    {
        #region Protected Constructors

        protected InvalidValidatorUsageException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

        #endregion Protected Constructors

        #region Public Constructors

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

        #endregion Public Constructors
    }
}