namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Reflection;

   public interface IHelpProvider
   {
      #region Public Methods and Operators

      void PrintTypeHelp(Type type);

      void PrintPropertyHelp(PropertyInfo property);

      #endregion
   }
}