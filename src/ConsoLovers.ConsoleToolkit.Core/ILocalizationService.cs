// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocalizationService.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

/// <summary>Service interface that is used to do localization</summary>
public interface ILocalizationService
{
   string GetLocalizedSting(string resourceKey);
}