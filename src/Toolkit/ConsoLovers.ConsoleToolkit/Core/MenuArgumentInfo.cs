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
      #region Constants and Fields

      private readonly ParameterInfo parameterInfo;

      #endregion

      #region Constructors and Destructors

      public MenuArgumentInfo([NotNull] ParameterInfo parameterInfo)
      {
         this.parameterInfo = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
         Required = (parameterInfo as ArgumentInfo)?.Attribute.Required ?? false;
         DisplayName = parameterInfo.ParameterName;

         var menuAttribute = parameterInfo.PropertyInfo.GetAttribute<MenuAttribute>();
         if (menuAttribute == null)
         {
            Visible = true;
         }
         else
         {
            Visible = menuAttribute.Visible;
            if (menuAttribute is MenuArgumentAttribute argumentAttribute)
            {
               DisplayName = argumentAttribute.DisplayName ?? parameterInfo.ParameterName;
               DisplayOrder = argumentAttribute.DisplayOrder;
               IsPassword = argumentAttribute.IsPassword;
            }
         }
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the display name.</summary>
      /// <value>The display name.</value>
      public string DisplayName { get; set; }

      /// <summary>Gets or sets the display order.</summary>
      public int DisplayOrder { get; set; }

      /// <summary>Gets or sets a value indicating whether this argument is a password.</summary>
      public bool IsPassword { get; set; }

      /// <summary>Gets or sets a value indicating whether this argument is required.</summary>
      public bool Required { get; set; }

      public bool Visible { get; }

      #endregion

      #region Public Methods and Operators

      public object GetValue([NotNull] object argumentInstance)
      {
         if (argumentInstance == null)
            throw new ArgumentNullException(nameof(argumentInstance));

         return parameterInfo.PropertyInfo.GetValue(argumentInstance);
      }

      public void SetValue([NotNull] object argumentInstance, object value)
      {
         if (argumentInstance == null)
            throw new ArgumentNullException(nameof(argumentInstance));

         var convertedValue = Convert.ChangeType(value, parameterInfo.PropertyInfo.PropertyType);
         parameterInfo.PropertyInfo.SetValue(argumentInstance, convertedValue);
      }

      #endregion
   }
}