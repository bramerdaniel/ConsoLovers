// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentList.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Collections.Generic;
using System.Linq;

public class CommandLineArgumentList : List<CommandLineArgument>, ICommandLineArguments
{
   #region Constructors and Destructors

   public CommandLineArgumentList()
      : this(StringComparer.InvariantCultureIgnoreCase)
   {
   }

   public CommandLineArgumentList(IEqualityComparer<string> comparer)
   {
      Comparer = comparer;
   }

   public CommandLineArgument this[string name] => this.FirstOrDefault(x => Comparer.Equals(x.Name, name));

   #endregion

   #region Properties

   internal IEqualityComparer<string> Comparer { get; }

   #endregion

   #region Public Methods and Operators

   public static CommandLineArgumentList FromDictionary(IDictionary<string, CommandLineArgument> arguments)
   {
      var equalityComparer = arguments is Dictionary<string, CommandLineArgument> dictionary
         ? dictionary.Comparer
         : StringComparer.InvariantCultureIgnoreCase;

      var argumentList = new CommandLineArgumentList(equalityComparer);
      foreach (var sourceElement in arguments)
      {
         var commandLineArgument = sourceElement.Value;

         if (commandLineArgument.Name == null)
            commandLineArgument.Name = sourceElement.Key;

         argumentList.Add(commandLineArgument);
      }

      return argumentList;
   }

   public bool ContainsName(string name)
   {
      return this.Any(x => Comparer.Equals(x.Name, name));
   }

   public void RemoveFirst(string name)
   {
      var argument = this.FirstOrDefault(x => Comparer.Equals(x.Name, name));
      if (argument != null)
         Remove(argument);
   }

   public bool TryGetValue(string name, out CommandLineArgument commandLineArgument)
   {
      commandLineArgument = this.FirstOrDefault(x => Comparer.Equals(x.Name, name));
      return commandLineArgument != null;
   }

   #endregion
}

public interface ICommandLineArguments : IList<CommandLineArgument>
{
   bool TryGetValue(string name, out CommandLineArgument argument);

   void RemoveFirst(string name);

   bool ContainsName(string name);

   CommandLineArgument this[string name] { get; }
}