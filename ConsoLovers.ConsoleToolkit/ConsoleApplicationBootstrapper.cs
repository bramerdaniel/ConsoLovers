namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using JetBrains.Annotations;

   public class ConsoleApplicationBootstrapper
   {
      private readonly Type applicationType;

      private Func<Type, object> createApplication;

      public ConsoleApplicationBootstrapper([NotNull] Type applicationType)
      {
         if (applicationType == null)
            throw new ArgumentNullException(nameof(applicationType));

         this.applicationType = applicationType;
      }

      public ConsoleApplicationBootstrapper CreateApplication(Func<Type, object> applicationBuilder)
      {
         if (applicationBuilder == null)
            throw new ArgumentNullException(nameof(applicationBuilder));

         createApplication = applicationBuilder;
         return this;
      }

      public void Run(string[] args)
      {
         if (createApplication == null)
            createApplication = new Factory().CreateInstance;

         var applicationManager = new ConsoleApplicationManager(createApplication);
         applicationManager.Run(applicationType, args);
      }

   }
}