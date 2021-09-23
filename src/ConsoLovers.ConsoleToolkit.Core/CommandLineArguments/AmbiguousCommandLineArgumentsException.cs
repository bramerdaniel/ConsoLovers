namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System;
    using System.Runtime.Serialization;

    public class AmbiguousCommandLineArgumentsException : CommandLineArgumentException
    {
        protected AmbiguousCommandLineArgumentsException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

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
    }
}