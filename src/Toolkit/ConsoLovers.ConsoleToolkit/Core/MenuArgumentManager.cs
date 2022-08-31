// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuArgumentManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   internal class MenuArgumentManager : IMenuArgumentManager
   {
      #region Constants and Fields

      private readonly Dictionary<Type, object> argumentCache = new Dictionary<Type, object>();

      private readonly IServiceProvider serviceProvider;

      #endregion

      #region Constructors and Destructors

      public MenuArgumentManager([NotNull] IServiceProvider serviceProvider)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      }

      #endregion

      #region IMenuArgumentManager Members

      /// <summary>Removes the specified argument type from the cache.</summary>
      /// <typeparam name="T"></typeparam>
      public void Remove<T>()
      {
         Remove(typeof(T));
      }

      /// <summary>Removes the specified argument type from the cache.</summary>
      /// <param name="argumentType">Type of the argument.</param>
      public void Remove(Type argumentType)
      {
         if (argumentType != null)
            argumentCache.Remove(argumentType);
      }

      /// <summary>Gets the or creates the instance of the specified argument type.</summary>
      /// <param name="argumentType">Type of the argument.</param>
      /// <returns>The argument instance</returns>
      public object GetOrCreate(Type argumentType)
      {
         if (!argumentCache.TryGetValue(argumentType, out var argument))
         {
            argument = serviceProvider.GetService(argumentType) ?? ActivatorUtilities.CreateInstance(serviceProvider, argumentType);
            argumentCache[argumentType] = argument;
         }

         return argument;
      }

      /// <summary>Gets the or creates the instance of the specified argument type <see cref="T"/>.</summary>
      /// <typeparam name="T"></typeparam>
      /// <returns>The argument instance</returns>
      public T GetOrCreate<T>()
      {
         return (T)GetOrCreate(typeof(T));
      }

      /// <summary>Clears all cached argument values.</summary>
      public void Clear()
      {
         argumentCache.Clear();
      }

      #endregion
   }
}