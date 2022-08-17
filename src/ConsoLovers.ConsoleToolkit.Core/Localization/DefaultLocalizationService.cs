// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultLocalizationService.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Localization;

using System;
using System.Collections.Generic;
using System.Resources;

using JetBrains.Annotations;

public class DefaultLocalizationService : ILocalizationService
{
   private readonly List<ResourceManager> resourceManagers = new();

   public string GetLocalizedSting(string resourceKey)
   {
      foreach (var manager in resourceManagers)
      {
         var result = manager.GetString(resourceKey);
         return result;
      }

      return resourceKey;
   }

   public void AddResourceManager([NotNull] ResourceManager resourceManager)
   {
      if (resourceManager == null)
         throw new ArgumentNullException(nameof(resourceManager));

      resourceManagers.Add(resourceManager);
   }
}