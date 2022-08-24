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

      public object GetOrCreate(Type argumentType)
      {
         if (!argumentCache.TryGetValue(argumentType, out var argument))
         {
            argument = serviceProvider.GetService(argumentType);
            argumentCache[argumentType] = argument;
         }

         return argument;
      }

      /// <summary>Clears all cached argument values.</summary>
      public void Clear()
      {
         argumentCache.Clear();
      }

      #endregion
   }
}