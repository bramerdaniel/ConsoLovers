// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuArgumentInitializer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   /// <summary>Can be implemented by a command to perform custom argument initialization</summary>
   public interface IMenuArgumentInitializer
   {
      /// <summary>Initializes the arguments of the command.</summary>
      /// <param name="context">The <see cref="IArgumentInitializationContext"/> that contains the data about the command that is about to be executed.</param>
      void InitializeArguments(IArgumentInitializationContext context);
   }
}