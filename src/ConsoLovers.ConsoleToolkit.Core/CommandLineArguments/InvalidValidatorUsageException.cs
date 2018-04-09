namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Runtime.Serialization;

   public class InvalidValidatorUsageException : CommandLineArgumentException
   {
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
   }
}