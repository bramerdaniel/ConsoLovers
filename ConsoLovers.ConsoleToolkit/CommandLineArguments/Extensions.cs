// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   public static class Extensions
   {
      public static bool Implements<T>(this Type type) where T : class
      {
         var interfaceType = typeof(T);
         if ((interfaceType == null) || !interfaceType.IsInterface)
            throw new ArgumentException("Only interfaces can be 'implemented'.");

         return type.IsAssignableFrom(interfaceType);
      }
   }
}