// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuArgumentInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   internal class MenuArgumentInfo
   {
      private readonly ParameterInfo parameterInfo;

      public MenuArgumentInfo([NotNull] ParameterInfo parameterInfo)
      {
         this.parameterInfo = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
      }

      public string DisplayName { get; set; }

      public void SetValue([NotNull] object argumentInstance, object value)
      {
         if (argumentInstance == null)
            throw new ArgumentNullException(nameof(argumentInstance));

         var convertedValue = Convert.ChangeType(value, parameterInfo.PropertyInfo.PropertyType);
         parameterInfo.PropertyInfo.SetValue(argumentInstance, convertedValue);
      }

      public object GetValue([NotNull] object argumentInstance)
      {
         if (argumentInstance == null)
            throw new ArgumentNullException(nameof(argumentInstance));

         return parameterInfo.PropertyInfo.GetValue(argumentInstance);
      }
   }
}