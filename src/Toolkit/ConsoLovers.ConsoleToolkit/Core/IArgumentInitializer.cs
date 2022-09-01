// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentInitializer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   public interface IArgumentInitializer
   {
      /// <summary>Initializes the arguments of the command.</summary>
      /// <param name="context">The <see cref="IArgumentInitializationContext"/> that contains the data about the command that is about to be executed.</param>
      void InitializeArguments(IArgumentInitializationContext context);
   }
}