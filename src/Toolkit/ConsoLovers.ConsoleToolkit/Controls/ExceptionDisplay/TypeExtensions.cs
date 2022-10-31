// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

public static class TypeExtensions
{
   public static string Alias(this Type type)
   {
      return TypeAliases.ContainsKey(type) ?
         TypeAliases[type] : string.Empty;
   }

   public static string AliasOrName(this Type type)
   {
      return TypeAliases.TryGetValue(type, out var alias) ? alias: type.Name;
   }

   private static readonly Dictionary<Type, string> TypeAliases = new()
   {
      { typeof(byte), "byte" },
      { typeof(sbyte), "sbyte" },
      { typeof(short), "short" },
      { typeof(ushort), "ushort" },
      { typeof(int), "int" },
      { typeof(uint), "uint" },
      { typeof(long), "long" },
      { typeof(ulong), "ulong" },
      { typeof(float), "float" },
      { typeof(double), "double" },
      { typeof(decimal), "decimal" },
      { typeof(object), "object" },
      { typeof(bool), "bool" },
      { typeof(char), "char" },
      { typeof(string), "string" },
      { typeof(void), "void" },
      { typeof(byte?), "byte?" },
      { typeof(sbyte?), "sbyte?" },
      { typeof(short?), "short?" },
      { typeof(ushort?), "ushort?" },
      { typeof(int?), "int?" },
      { typeof(uint?), "uint?" },
      { typeof(long?), "long?" },
      { typeof(ulong?), "ulong?" },
      { typeof(float?), "float?" },
      { typeof(double?), "double?" },
      { typeof(decimal?), "decimal?" },
      { typeof(bool?), "bool?" },
      { typeof(char?), "char?" }
   };
}