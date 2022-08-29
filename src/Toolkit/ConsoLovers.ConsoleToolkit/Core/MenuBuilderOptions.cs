// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBuilderOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   /// <summary>NotSpecified implementation of the <see cref="IMenuBuilderOptions"/> interface</summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IMenuBuilderOptions" />
   internal class MenuBuilderOptions : IMenuBuilderOptions
   {
      public ArgumentInitializationModes ArgumentInitializationMode { get; set; } = ArgumentInitializationModes.WhileExecution;

      public MenuBuilderBehaviour MenuBehaviour { get; set; } = MenuBuilderBehaviour.ShowAllCommand;
   }
}