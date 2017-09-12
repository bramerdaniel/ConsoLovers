namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>Data class containing all information for a defined command line parameter in its according data class</summary>
   [DebuggerDisplay("[{" + nameof(ParameterName) + "}]")]
   public abstract class ParameterInfo
   {
      protected ParameterInfo([NotNull] PropertyInfo propertyInfo, [NotNull] CommandLineAttribute commandLineAttribute)
      {
         if (propertyInfo == null)
            throw new ArgumentNullException(nameof(propertyInfo));
         if (commandLineAttribute == null)
            throw new ArgumentNullException(nameof(commandLineAttribute));

         PropertyInfo = propertyInfo;
         ParameterType = propertyInfo.PropertyType;
         CommandLineAttribute = commandLineAttribute;
         Identifiers = commandLineAttribute.GetIdentifiers().ToArray();
         ParameterName = commandLineAttribute.Name;
      }

      public string ParameterName { get; }

      #region Public Properties
      public PropertyInfo PropertyInfo { get; }

      protected CommandLineAttribute CommandLineAttribute { get; }

      /// <summary>Gets the type of the property that was decorated with the <see cref="CommandLineAttribute"/>.</summary>
      public Type ParameterType { get; }

      /// <summary>Gets the defined names.</summary>
      public string[] Identifiers{ get; }
      
      #endregion
   }
}