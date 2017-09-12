namespace XCopyDemo
{
   using System;

   internal class XCopyException : Exception
   {
      public XCopyException()
      {
      }

      public XCopyException(string message)
         : base(message)
      {
      }

      public XCopyException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}