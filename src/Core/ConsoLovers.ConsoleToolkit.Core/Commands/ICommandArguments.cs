// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   public interface ICommandArguments<T>
   {
      /// <summary>Gets or sets the arguments that were specified for the command.</summary>
      T Arguments { get; set; }
   }
}