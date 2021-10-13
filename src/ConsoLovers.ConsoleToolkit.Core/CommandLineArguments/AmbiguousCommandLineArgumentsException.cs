namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System;
    using System.Runtime.Serialization;

    public class AmbiguousCommandLineArgumentsException : CommandLineArgumentException
    {
        #region Protected Constructors

        protected AmbiguousCommandLineArgumentsException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

        #endregion Protected Constructors

        #region Public Constructors

        public AmbiguousCommandLineArgumentsException()
        {
        }

        public AmbiguousCommandLineArgumentsException(string message)
           : base(message)
        {
        }

        public AmbiguousCommandLineArgumentsException(string message, Exception innerException)
           : base(message, innerException)
        {
        }

        #endregion Public Constructors
    }
}