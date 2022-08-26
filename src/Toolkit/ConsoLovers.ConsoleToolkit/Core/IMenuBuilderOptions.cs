// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuBuilderOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   public interface IMenuBuilderOptions
   {
      ArgumentInitializationModes DefaultArgumentInitializationMode { get; set; }

      MenuBuilderBehaviour MenuBehaviour { get; set; }
   }
}