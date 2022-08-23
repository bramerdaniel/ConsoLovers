namespace ConsoLovers.ConsoleToolkit.Core
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