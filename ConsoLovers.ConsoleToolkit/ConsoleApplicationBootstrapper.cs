namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.DIContainer;

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
         if (createApplication != null)
            throw new InvalidOperationException("ApplicationBuilder function was already specified.");

         createApplication = applicationBuilder;
         return this;
      }

      public ConsoleApplicationBootstrapper UsingFactory([NotNull] IObjectFactory container)
      {
         if (container == null)
            throw new ArgumentNullException(nameof(container));
         if (createApplication != null)
            throw new InvalidOperationException("ApplicationBuilder function was already specified.");

         createApplication = container.CreateInstance;
         return this;
      }

      public void Run(string[] args)
      {
         if (createApplication == null)
            createApplication = new DefaultFactory().CreateInstance;

         var applicationManager = new ConsoleApplicationManager(createApplication);
         applicationManager.Run(applicationType, args);
      }

   }
}