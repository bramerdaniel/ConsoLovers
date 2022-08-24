// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System.Reflection;
   using JetBrains.Annotations;

   /// <summary>
   /// 
   /// </summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ParameterInfo" />
   public class OptionInfo : ParameterInfo
   {
      #region Constructors and Destructors

      public OptionInfo([NotNull] PropertyInfo propertyInfo, [NotNull] OptionAttribute commandLineAttribute)
         : base(propertyInfo, commandLineAttribute)
      {
      }

      #endregion

      #region Public Properties

      public OptionAttribute Attribute => (OptionAttribute)CommandLineAttribute;

      #endregion
   }
}