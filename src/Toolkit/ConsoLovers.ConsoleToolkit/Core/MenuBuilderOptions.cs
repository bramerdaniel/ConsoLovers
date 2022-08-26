// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBuilderOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   /// <summary>Default implementation of the <see cref="IMenuBuilderOptions"/> interface</summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IMenuBuilderOptions" />
   internal class MenuBuilderOptions : IMenuBuilderOptions
   {
      public ArgumentInitializationModes DefaultArgumentInitializationMode { get; set; }

      public MenuBuilderBehaviour MenuBehaviour { get; set; }
   }
}