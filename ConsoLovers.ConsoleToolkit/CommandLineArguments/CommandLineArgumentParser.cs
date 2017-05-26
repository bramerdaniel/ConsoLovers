// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentParser.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Globalization;
   using System.Linq;

   public class CommandLineArgumentParser
   {
      #region Constants and Fields

      private static readonly char[] ArgumentSigns = { '-', '/' };

      private static readonly char[] NameSeparators = { '=', ':' };

      #endregion

      #region Public Methods and Operators

      /// <summary>Normalizes the arguments.</summary>
      /// <param name="args">The arguments.</param>
      /// <returns>A normalized enumerable</returns>
      public IEnumerable<string> NormalizeArguments(params string[] args)
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

      #endregion

      #region Methods

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

      private static bool IsQuoted(string argumentString)
      {
         return argumentString.StartsWith("\"");
      }

      private static bool IsNameSeparator(string candidate)
      {
         return candidate.Length == 1 && NameSeparators.Contains(candidate[0]);
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

         arguments[name] = new CommandLineArgument { Name = name, Value = valueString, Index = index };
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

         arguments.Add(option, new CommandLineArgument { Name = option, Value = "true", Index = index });
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

      #endregion
   }
}