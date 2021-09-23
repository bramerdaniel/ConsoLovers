namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System;
    using System.Reflection;

    public interface IHelpProvider
    {
        #region Public Methods and Operators

        void PrintPropertyHelp(PropertyInfo property);

        void PrintTypeHelp(Type type);

        #endregion Public Methods and Operators
    }
}