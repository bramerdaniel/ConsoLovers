// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

public static class ReflectionExtensions
{
   internal static PropertyInfo[] GetProperties([JetBrains.Annotations.NotNull] this Type type, Modifiers modifiers)
   {
      if (type == null)
         throw new ArgumentNullException(nameof(type));

      return type.GetPropertiesInternal(modifiers).ToArray();
   }

   internal static PropertyInfo[] GetProperties(this Type type)
   {
      return type.GetProperties(Modifiers.PublicOrInternal);
   }

   [SuppressMessage("sonar", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields")]
   internal static IEnumerable<PropertyInfo> GetPropertiesInternal(this Type type, Modifiers modifiers)
   {

      foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
      {
         var setter = property.GetSetMethod(true);
         if (modifiers.HasFlag(Modifiers.Public) && IsPublic(setter))
         {
            yield return property;
         }
         else if (modifiers.HasFlag(Modifiers.Internal) && IsInternal(setter))
         {
            yield return property;
         }
         else if (modifiers.HasFlag(Modifiers.Private) && setter?.IsPrivate == true)
         {
            yield return property;
         }
      }
   }

   private static bool IsPublic(MethodInfo setter)
   {
      if (setter.IsPublic)
         return true;
      return false;

   }

   private static bool IsInternal(MethodInfo setter)
   {
      // only internal
      if (setter?.IsAssembly == true)
         return true;

      // protected internal
      if (setter?.IsFamilyOrAssembly == true)
         return true;

      return false;
   }
}

[Flags]
internal enum Modifiers
{
   None = 0,
   Public = 0x1,
   Internal = 0x2,
   Private = 0x4,
   PublicOrInternal = Public | Internal
}