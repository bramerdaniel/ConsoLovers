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
   #region Constants and Fields

   private readonly List<ResourceManager> resourceManagers = new();

   #endregion

   #region ILocalizationService Members

   public string GetLocalizedSting(string resourceKey)
   {
      return GetLocalizedSting(resourceKey, resourceKey);
   }

   public string GetLocalizedSting(string resourceKey, string fallbackValue)
   {
      foreach (var manager in resourceManagers)
      {
         var result = manager.GetString(resourceKey);
         if (result != null)
            return result;
      }

      return fallbackValue;
   }

   #endregion

   #region Public Methods and Operators

   public void AddResourceManager([NotNull] ResourceManager resourceManager)
   {
      if (resourceManager == null)
         throw new ArgumentNullException(nameof(resourceManager));

      resourceManagers.Add(resourceManager);
   }

   public void RemovedResourceManager([NotNull] ResourceManager resourceManager)
   {
      if (resourceManager == null)
         throw new ArgumentNullException(nameof(resourceManager));

      resourceManagers.Remove(resourceManager);
   }

   #endregion
}