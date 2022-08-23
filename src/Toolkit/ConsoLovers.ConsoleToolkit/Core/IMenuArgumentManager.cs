// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuArgumentManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Collections.Generic;

   using JetBrains.Annotations;

   public interface IMenuArgumentManager
   {
      object GetOrCreate(Type argumentType);

   }

   internal class MenuArgumentManager : IMenuArgumentManager
   {
      private readonly IServiceProvider serviceProvider;

      private readonly Dictionary<Type, object> argumentCache = new Dictionary<Type, object>();

      public MenuArgumentManager([NotNull] IServiceProvider serviceProvider)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      }

      public object GetOrCreate(Type argumentType)
      {
         if (!argumentCache.TryGetValue(argumentType, out var argument))
         {
            argument = serviceProvider.GetService(argumentType);
            argumentCache[argumentType] = argument;
         }

         return argument;
      }
   }
}