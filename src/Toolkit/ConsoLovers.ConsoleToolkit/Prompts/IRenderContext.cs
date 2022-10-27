// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts
{
   using ConsoLovers.ConsoleToolkit.Core;

   public interface IRenderContext
   {
      /// <summary>Gets the console.</summary>
      IConsole Console { get; }
   }
}