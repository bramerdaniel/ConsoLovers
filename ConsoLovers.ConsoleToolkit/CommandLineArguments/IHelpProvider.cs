namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Reflection;
   using System.Resources;

   public interface IHelpProvider
   {
      #region Public Methods and Operators

      void PrintTypeHelp(Type type);

      void PrintPropertyHelp(PropertyInfo property);

      #endregion
   }
}