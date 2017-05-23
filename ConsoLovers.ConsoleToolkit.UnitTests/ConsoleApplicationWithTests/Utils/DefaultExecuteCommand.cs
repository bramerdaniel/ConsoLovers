namespace ConsoLovers.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using JetBrains.Annotations;

   public class DefaultExecuteCommand : ICommand
   {
      private readonly ICommandVerification verification;

      public DefaultExecuteCommand([NotNull] ICommandVerification verification)
      {
         if (verification == null)
            throw new ArgumentNullException(nameof(verification));

         this.verification = verification;
      }

      public void Execute()
      {
         verification.Execute("DefaultExecute");
      }
   }
}