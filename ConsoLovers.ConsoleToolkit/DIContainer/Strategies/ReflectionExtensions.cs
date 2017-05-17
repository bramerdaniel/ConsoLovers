// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer.Strategies
{
   using System;
   using System.Linq;
   using System.Reflection;

   /// <summary>Helper extensions for reflection api</summary>
   public static class ReflectionExtensions
   {
      #region Public Methods and Operators

      /// <summary>Determines whether the specified constructor has an attribute of the given type.</summary>
      /// <param name="constructor">The constructor.</param>
      /// <param name="attributeType">Type of the attribute.</param>
      /// <returns>
      ///    <c>true</c> if the specified constructor has the attribute; otherwise, <c>false</c>.
      /// </returns>
      public static bool HasAttribute(this ConstructorInfo constructor, Type attributeType)
      {
         return constructor.GetCustomAttributes(attributeType, true).Length > 0;
      }

      /// <summary>Determines whether the specified constructor has an attribute of the given type.</summary>
      /// <param name="property">The constructor.</param>
      /// <param name="attributeType">Type of the attribute.</param>
      /// <returns>
      ///    <c>true</c> if the specified constructor has the attribute; otherwise, <c>false</c>.
      /// </returns>
      public static bool HasAttribute(this PropertyInfo property, Type attributeType)
      {
         return property.GetCustomAttributes(attributeType, true).Length > 0;
      }

      /// <summary>Determines whether the property has a public getter</summary>
      /// <param name="property">The property to check.</param>
      /// <returns>
      ///    <c>true</c> if it has a public getter ; otherwise, <c>false</c>.
      /// </returns>
      public static bool HasPublicGetter(this PropertyInfo property)
      {
         return property.GetAccessors().Any(acc => acc.Name.StartsWith("get_"));
      }

      /// <summary>Determines whether the property has a public setter</summary>
      /// <param name="property">The property to check.</param>
      /// <returns>
      ///    <c>true</c> if it has a public setter] ; otherwise, <c>false</c>.
      /// </returns>
      public static bool HasPublicSetter(this PropertyInfo property)
      {
         return property.GetAccessors().Any(acc => acc.Name.StartsWith("set_"));
      }

      #endregion
   }
}