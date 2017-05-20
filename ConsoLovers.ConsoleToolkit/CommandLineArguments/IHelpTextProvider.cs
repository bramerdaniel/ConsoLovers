namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Resources;

   public interface IHelpTextProvider
   {
      #region Public Methods and Operators

      void Initialize(Type helpType, ResourceManager resourceManager);

      void WriteArguments();

      void WriteFooter();

      void WriteHeader();

      #endregion
   }
}