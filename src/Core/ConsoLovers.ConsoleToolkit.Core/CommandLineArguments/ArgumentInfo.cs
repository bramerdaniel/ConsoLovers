// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System.Linq;
   using System.Reflection;
   using JetBrains.Annotations;

   /// <summary>Helper class providing information about a command line parameter, that was decorated with the <see cref="ArgumentAttribute"/></summary>
   /// <seealso cref="ParameterInfo"/>
   public class ArgumentInfo : ParameterInfo
   {
      #region Constructors and Destructors

      internal ArgumentInfo([NotNull] PropertyInfo propertyInfo, [NotNull] ArgumentAttribute commandLineAttribute)
         : base(propertyInfo, commandLineAttribute)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the attribute.</summary>
      public ArgumentAttribute Attribute => (ArgumentAttribute)CommandLineAttribute;
      
      public ArgumentValidatorAttribute ValidatorAttribute => PropertyInfo.GetCustomAttributes<ArgumentValidatorAttribute>(true).FirstOrDefault();

      #endregion
   }
}