// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineArgumentParser.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   /// <summary>Interface that creates a command line dictionary from the given command line arguments</summary>
   public interface ICommandLineArgumentParser
   {
      /// <summary>Parses the given arguments into a dictionary.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the arguments should be treated case sensitive.</param>
      /// <returns>The created dictionary</returns>
      CommandLineArgumentList ParseArguments(string[] args, bool caseSensitive);


      // TODO: remove caseSensitive or do it the right way
      CommandLineArgumentList ParseArguments(string args, bool caseSensitive);

      /// <summary>Parses the given arguments into a dictionary.</summary>
      /// <param name="args">The command line arguments as string.</param>
      /// <returns>The <see cref="CommandLineArgumentList"/></returns>
      CommandLineArgumentList ParseArguments(string args);

      /// <summary>Parses the given arguments into a dictionary.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <returns>The <see cref="CommandLineArgumentList"/></returns>
      CommandLineArgumentList ParseArguments(string[] args);
   }
}