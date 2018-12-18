// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationVerification.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public interface IApplicationVerification<in T>
   {
      #region Public Methods and Operators

      void Argument(string name, object value);

      /// <summary>Gets called when a command line parameter was mapped to a specific property.</summary>
      /// <param name="propertyName">The name of the parameters.</param>
      /// <param name="value">The value of the parameters.</param>
      void MappedCommandLineParameter(string propertyName, object value);

      
      void MappedCommandLineParameter(PropertyInfo property);

      void MappedCommandLineParameter(PropertyInfo property, object value);

      /// <summary>Gets called when the .</summary>
      /// <returns></returns>
      string Run();

      string RunWith(T arguments);

      string RunWithCommand(ICommand command);

      string RunWithoutArguments();

      /// <summary>Gets called when a command line parameter was not mapped to a specific property.</summary>
      /// <param name="name">The name of the parameters.</param>
      /// <param name="value">The value of the parameters.</param>
      void UnmappedCommandLineParameter(string name, string value);

      #endregion
   }
}