namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using JetBrains.Annotations;

   public class FluentConsoleApplicationManager
   {
      private readonly Type applicationType;

      private IEngineFactory engineFactory;

      public FluentConsoleApplicationManager([NotNull] Type applicationType)
      {
         if (applicationType == null)
            throw new ArgumentNullException(nameof(applicationType));

         this.applicationType = applicationType;
      }

      public FluentConsoleApplicationManager UsingFactory([NotNull] IEngineFactory factory)
      {
         if (factory == null)
            throw new ArgumentNullException(nameof(factory));

         engineFactory = factory;

         return this;
      }

      public void Run(string[] args)
      {
         var applicationManager = new ConsoleApplicationManager(engineFactory ??  new EngineFactory());
         applicationManager.Run(applicationType, args);
      }

   }
}