namespace ConsoLovers
{
   using System;

   public class ExceptionEventArgs
   {
      public Exception Exception { get; }

      public bool Handled { get; set; }

      public ExceptionEventArgs(Exception ex)
      {
         if (ex == null)
            throw new ArgumentNullException(nameof(ex));
         Exception = ex;
      }
   }
}