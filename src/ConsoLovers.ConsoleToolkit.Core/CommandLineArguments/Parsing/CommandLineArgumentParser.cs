﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentParser.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing
{
   using System;
   using System.Collections.Generic;
   using System.Globalization;
   using System.Linq;
   using System.Text;

   /// <summary>Default implementation of the <see cref="ICommandLineArgumentParser"/> interface</summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ICommandLineArgumentParser"/>
   public class CommandLineArgumentParser : ICommandLineArgumentParser
   {
      #region Constants and Fields

      private static readonly char[] ArgumentSigns = { '-', '/' };

      private static readonly char[] NameSeparators = { '=', ':' };

      #endregion

      #region ICommandLineArgumentParser Members

      /// <summary>Parses the given arguments into a dictionary.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the arguments should be treated case sensitive.</param>
      /// <returns>The created dictionary</returns>
      public IDictionary<string, CommandLineArgument> ParseArguments(string[] args, bool caseSensitive)
      {
         var arguments = new Dictionary<string, CommandLineArgument>(caseSensitive ? StringComparer.InvariantCulture : StringComparer.CurrentCultureIgnoreCase);
         int index = 0;

         foreach (string argument in NormalizeArguments(args).Where(x => !string.IsNullOrEmpty(x)))
         {
            if (IsNamedParameter(argument))
               ParseNamedParameter(argument, arguments, index);
            else
               ParseOption(argument, arguments, index);

            index++;
         }

         return arguments;
      }

      public IDictionary<string, CommandLineArgument> ParseArguments(string args, bool caseSensitive)
      {
         int skippFirst = args.Equals(Environment.CommandLine) ? 1 : 0;

         var arguments = new Dictionary<string, CommandLineArgument>(caseSensitive ? StringComparer.InvariantCulture : StringComparer.InvariantCultureIgnoreCase);
         int index = 0;
         foreach (var arg in SplitIntoArgs(args).Skip(skippFirst))
         {
            var commandLineArgument = ParseSingleArgument(arg, index);
            arguments[commandLineArgument.Name] = commandLineArgument;
            index++;
         }

         return arguments;
      }

      public IDictionary<string, CommandLineArgument> ParseArguments(string args)
      {
         return ParseArguments(args, false);
      }

      #endregion

      #region Methods

      /// <summary>Normalizes the arguments.</summary>
      /// <param name="args">The arguments.</param>
      /// <returns>A normalized enumerable</returns>
      internal IEnumerable<string> NormalizeArguments(params string[] args)
      {
         var normalized = new List<string>();
         var maxIndex = args.Length - 1;

         for (int i = 0; i < args.Length; i++)
         {
            var current = args[i].Trim();
            var candidate = maxIndex >= (i + 1) ? args[i + 1].Trim() : string.Empty;
            var next = maxIndex >= (i + 2) ? args[i + 2].Trim() : string.Empty;

            if (IsNameSeparator(candidate))
            {
               normalized.Add($"{current}{candidate}{next}");
               i += 2;
            }
            else
            {
               if (EndsWithNameSeparator(current) || StartsWithNameSeparator(candidate))
               {
                  normalized.Add($"{current}{candidate}");
                  i++;
               }
               else
               {
                  normalized.Add(current);
               }
            }
         }

         return normalized;
      }

      private static bool EndsWithNameSeparator(string current)
      {
         return !string.IsNullOrEmpty(current) && NameSeparators.Contains(current[current.Length - 1]);
      }

      private static int GetSplitIndex(string argumentString)
      {
         int splitIndex = int.MaxValue;
         foreach (var nameSeparator in NameSeparators)
         {
            var candidate = argumentString.IndexOf(nameSeparator);
            if (candidate >= 0)
               splitIndex = Math.Min(splitIndex, candidate);
         }

         return splitIndex == int.MaxValue ? -1 : splitIndex;
      }

      private static bool IsNamedParameter(string argumentString)
      {
         return !IsQuoted(argumentString) && NameSeparators.Any(argumentString.Contains);
      }

      private static bool IsNameSeparator(string candidate)
      {
         return candidate.Length == 1 && NameSeparators.Contains(candidate[0]);
      }

      private static bool IsQuoted(string argumentString)
      {
         return argumentString.StartsWith("\"");
      }

      private static void ParseNamedParameter(string argumentString, IDictionary<string, CommandLineArgument> arguments, int index)
      {
         if (string.IsNullOrEmpty(argumentString))
            return;

         string[] tokens = Split(argumentString);
         if (tokens.Length < 2)
            throw new CommandLineArgumentException(string.Format(CultureInfo.InvariantCulture, "The argument \"{0}\" is invalid.", argumentString));

         string name = tokens[0].TrimStart(ArgumentSigns).Trim();
         if (string.IsNullOrEmpty(name))
            throw new CommandLineArgumentException(string.Format(CultureInfo.InvariantCulture, "The argument \"{0}\" is invalid.", argumentString));

         string valueString = tokens[1].Trim();

         if (arguments.ContainsKey(name))
         {
            throw new CommandLineArgumentException(string.Format(CultureInfo.InvariantCulture, "The argument \"{0}\" occurs more than once.", argumentString));
         }

         arguments[name] = new CommandLineArgument { Name = name, Value = valueString, Index = index, OriginalString = argumentString };
      }

      private static void ParseOption(string argumentString, IDictionary<string, CommandLineArgument> arguments, int index)
      {
         if (string.IsNullOrEmpty(argumentString))
            return;

         string option = argumentString.TrimStart(ArgumentSigns).Trim();
         if (string.IsNullOrEmpty(option))
            return;

         if (arguments.ContainsKey(option))
         {
            throw new CommandLineArgumentException(string.Format(CultureInfo.InvariantCulture, "The option \"{0}\" occurs more than once.", argumentString));
         }

         arguments.Add(option, new CommandLineArgument { Name = option, Value = null, Index = index, OriginalString = argumentString });
      }

      private static string[] Split(string argumentString)
      {
         int index = GetSplitIndex(argumentString);
         var name = argumentString.Substring(0, index);
         var valueStart = index + 1;
         var value = valueStart >= argumentString.Length ? string.Empty : argumentString.Substring(valueStart);
         return new[] { name, value };
      }

      private static bool StartsWithNameSeparator(string candidate)
      {
         return !string.IsNullOrEmpty(candidate) && NameSeparators.Contains(candidate[0]);
      }

      private bool IsNameSeparator(char character)
      {
         return NameSeparators.Contains(character);
      }

      private CommandLineArgument ParseSingleArgument(string argument, int index)
      {
         StringBuilder nameBuilder = new StringBuilder();
         StringBuilder valueBuilder = null;
         bool inName = true;
         bool foundNameSeparator = false;

         foreach (var charInfo in new CharRope(argument))
         {
            if (charInfo.IsQuote() && !charInfo.IsEscaped())
               continue;

            if (!charInfo.InsideQuotes() && !foundNameSeparator && IsNameSeparator(charInfo.Current))
            {
               inName = false;
               foundNameSeparator = true;
            }
            else
            {
               if (!charInfo.IsFirst() || !ArgumentSigns.Contains(charInfo.Current))
                  AppendCharacter(charInfo);
            }
         }

         var name = nameBuilder.ToString();
         return new CommandLineArgument
         {
            Index = index, 
            Name = name, 
            Value = valueBuilder?.ToString(), 
            OriginalString = argument
         };

         void AppendCharacter(CharInfo charInfo)
         {
            if (charInfo.Current == '\\' && (charInfo.Next == '\\' || charInfo.Next == '"'))
               return;

            if (inName)
            {
               nameBuilder.Append(charInfo.Current);
            }
            else
            {
               GetValueBuilder().Append(charInfo.Current);
            }
         }

         StringBuilder GetValueBuilder()
         {
            return valueBuilder ?? (valueBuilder = new StringBuilder());
         }
      }

      private IEnumerable<string> SplitIntoArgs(string args)
      {
         StringBuilder builder = new StringBuilder();
         foreach (var charInfo in new CharRope(args))
         {
            if (charInfo.IsWhiteSpace() && !charInfo.InsideQuotes())
            {
               if (builder.Length > 0)
               {
                  yield return builder.ToString();
                  builder = new StringBuilder();
               }
            }
            else
            {
               builder.Append(charInfo.Current);
            }
         }

         if (builder.Length > 0)
            yield return builder.ToString();
      }

      #endregion
   }
}