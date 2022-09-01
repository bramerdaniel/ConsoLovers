// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentInitializationContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IArgumentInitializationContext
   {
      /// <summary>Gets the menu item that triggered the execution of the command.</summary>
      ConsoleMenuItem MenuItem { get; }

      /// <summary>Initializes the argument with the specified name.</summary>
      /// <param name="argumentName">Name of the argument to initialize.</param>
      object InitializeArgument(string argumentName);
      
      /// <summary>Creates the arguments instance for the current command.</summary>
      object GetOrCreateArguments();

      /// <summary>Initializes all visible arguments.</summary>
      void InitializeArguments();
   }
}