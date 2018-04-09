namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Runtime.Serialization;

   public class CommandLineArgumentValidationException : CommandLineArgumentException
   {
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
   }
}