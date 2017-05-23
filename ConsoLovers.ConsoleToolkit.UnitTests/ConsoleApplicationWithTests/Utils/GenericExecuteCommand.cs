namespace ConsoLovers.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using JetBrains.Annotations;

   public class GenericExecuteCommand : ICommand<TestCommandArguments>
   {
      private readonly ICommandVerification verification;

      private TestCommandArguments arguments;

      public GenericExecuteCommand([NotNull] ICommandVerification verification)
      {
         if (verification == null)
            throw new ArgumentNullException(nameof(verification));

         this.verification = verification;
      }

      public void Execute()
      {
         verification.Execute("GenericExecute");
      }

      public TestCommandArguments Arguments
      {
         get
         {
            return arguments;
         }
         set
         {
            if (value.String != null)
               verification.Argument("string", value.String);

            if (value.Int > 0)
               verification.Argument("int", value.Int);

            arguments = value;
         }
      }
   }
}