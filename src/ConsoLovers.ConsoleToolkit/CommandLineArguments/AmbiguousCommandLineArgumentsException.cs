namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Runtime.Serialization;

   public class AmbiguousCommandLineArgumentsException : CommandLineArgumentException
   {
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

      protected AmbiguousCommandLineArgumentsException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}