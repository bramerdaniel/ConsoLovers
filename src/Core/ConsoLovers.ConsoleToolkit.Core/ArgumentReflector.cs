// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentReflector.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

/// <summary>Default implementation of the <see cref="IArgumentReflector"/> interface</summary>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IArgumentReflector"/>
internal class ArgumentReflector : IArgumentReflector
{
   #region Constants and Fields

   private readonly Dictionary<Type, ArgumentClassInfo> typeCache = new();

   #endregion

   #region IArgumentReflector Members

   /// <summary>Gets the argument type information.</summary>
   /// <typeparam name="T">The type of the argument</typeparam>
   /// <returns>The <see cref="ArgumentClassInfo"/> for the type</returns>
   public ArgumentClassInfo GetTypeInfo<T>()
   {
      return GetTypeInfo(typeof(T));
   }

   /// <summary>Gets the argument type information.</summary>
   /// <param name="argumentType">Type of the argument.</param>
   /// <returns>The <see cref="ArgumentClassInfo"/> for the type</returns>
   public ArgumentClassInfo GetTypeInfo(Type argumentType)
   {
      if (!typeCache.TryGetValue(argumentType, out var classInfo))
      {
         classInfo = new ArgumentClassInfo(argumentType);
         typeCache[argumentType] = classInfo;
      }

      return classInfo;
   }

   #endregion
}