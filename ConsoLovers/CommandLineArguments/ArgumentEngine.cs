// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentEngine.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Globalization;
   using System.Linq;
   using System.Reflection;
   using System.Resources;
   using System.Text;

   /// <summary>Parser class that can parse command line arguments</summary>
   public class ArgumentEngine
   {
      #region Constants and Fields

      private static readonly char[] ArgumentSigns = { '-', '/' };

      private static readonly char[] NameSeparators = { '=', ':' };

      #endregion

      #region Public Methods and Operators

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for</typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      /// <param name="consoleWidth">Width of the console.</param>
      /// <returns>A <see cref="StringBuilder"/> containing the formatted help text.</returns>
      public StringBuilder FormatHelp<T>(ResourceManager resourceManager, int consoleWidth)
      {
         var stringBuilder = new StringBuilder();
         var argumentHelps = GetHelp<T>(resourceManager).ToList();
         int longestNameWidth = argumentHelps.Select(a => a.PropertyName.Length).Max() + 2;
         int longestAliasWidth = argumentHelps.Select(a => a.AliasString.Length).Max() + 4;

         int descriptionWidth = consoleWidth - longestNameWidth - longestAliasWidth;
         int leftWidth = consoleWidth - descriptionWidth;

         foreach (ArgumentHelp argumentHelp in argumentHelps)
         {
            var name = $"-{argumentHelp.PropertyName}".PadRight(longestNameWidth);
            var aliasString = $"[{argumentHelp.AliasString}]".PadRight(longestAliasWidth);

            stringBuilder.AppendFormat("{0}{1}", name, aliasString);

            var descriptionLines = GetWrappedStrings(argumentHelp.Description, descriptionWidth).ToList();

            stringBuilder.AppendLine(descriptionLines[0]);
            foreach (var part in descriptionLines.Skip(1))
            {
               stringBuilder.AppendLine(" ".PadLeft(leftWidth) + part);
            }
         }

         return stringBuilder;
      }

      /// <summary>Gets the help information for the class of the given type.</summary>
      /// <typeparam name="T">The argument class for creating the help for</typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      public IEnumerable<ArgumentHelp> GetHelp<T>(ResourceManager resourceManager)
      {
         PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
         foreach (PropertyInfo info in properties)
         {
            var argumentAttribute = (ArgumentAttribute)info.GetCustomAttributes(typeof(ArgumentAttribute), true).FirstOrDefault();
            var optionAttribute = (OptionAttribute)info.GetCustomAttributes(typeof(OptionAttribute), true).FirstOrDefault();
            var commandAttribute = (CommandAttribute)info.GetCustomAttributes(typeof(CommandAttribute), true).FirstOrDefault();
            var helpText = (HelpTextAttribute)info.GetCustomAttributes(typeof(HelpTextAttribute), true).FirstOrDefault();
            if (helpText != null)
            {
               yield return
                  new ArgumentHelp
                  {
                     PropertyName = GetArgumentName(info, argumentAttribute, optionAttribute, commandAttribute),
                     Aliases = GetAliases(argumentAttribute, optionAttribute, commandAttribute),
                     UnlocalizedDescription = helpText.Description,
                     LocalizedDescription = resourceManager?.GetString(helpText.ResourceKey)
                  };
            }
         }
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <param name="args">The arguments as <see cref="Dictionary{TKey,TValue}"/> that should be mapped to the instance.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(T instance, IDictionary<string, string> args)
      {
         var mapper = new ArgumentMapper<T>();
         return mapper.Map(instance, args);
      }

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for </typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      public void PrintHelp<T>(ResourceManager resourceManager)
      {
         var argumentHelps = GetHelp<T>(resourceManager).ToList();
         int longestNameWidth = argumentHelps.Select(a => a.PropertyName.Length).Max() + 2;
         int longestAliasWidth = argumentHelps.Select(a => a.AliasString.Length).Max() + 4;
         var consoleWidth = Console.WindowWidth;

         int descriptionWidth = consoleWidth - longestNameWidth - longestAliasWidth;
         int leftWidth = consoleWidth - descriptionWidth;

         foreach (ArgumentHelp argumentHelp in argumentHelps)
         {
            var name = $"-{argumentHelp.PropertyName}".PadRight(longestNameWidth);
            var aliasString = $"[{argumentHelp.AliasString}]".PadRight(longestAliasWidth);

            Console.Write("{0}{1}", name, aliasString);

            var descriptionLines = GetWrappedStrings(argumentHelp.Description, descriptionWidth).ToList();

            Console.WriteLine(descriptionLines[0]);
            foreach (var part in descriptionLines.Skip(1))
            {
               Console.WriteLine(" ".PadLeft(leftWidth) + part);
            }
         }
      }

      /// <summary>Maps the specified arguments to a class of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args)
      {
         return Map<T>(args, false);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args, bool caseSensitive)
      {
         var arguments = Parse(args, caseSensitive);
         var mapper = new ArgumentMapper<T>();

         return mapper.Map(arguments);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="unmappedArguments">Dictionary containing the arguments that could not be mapped.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(T instance, string[] args, out IDictionary<string, string> unmappedArguments)
      {
         return Map(instance, args, false, out unmappedArguments);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <param name="unmappedArguments">Dictionary containing the arguments that could not be mapped.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(T instance, string[] args, bool caseSensitive, out IDictionary<string, string> unmappedArguments)
      {
         var arguments = Parse(args, caseSensitive);
         var mapper = new ArgumentMapper<T>();

         var result = mapper.Map(instance, arguments);
         unmappedArguments = arguments;

         return result;
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(T instance, string[] args)
      {
         return Map(instance, args, false);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(T instance, string[] args, bool caseSensitive)
      {
         var arguments = Parse(args, caseSensitive);
         var mapper = new ArgumentMapper<T>();
         return mapper.Map(instance, arguments);
      }

      /// <summary>Parses the specified arguments.</summary>
      /// <param name="args">The arguments to parse.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>A <see cref="IDictionary{TKey,TValue}"/> the arguments were parsed into</returns>
      public IDictionary<string, string> Parse(string[] args, bool caseSensitive)
      {
         var arguments = new Dictionary<string, string>(caseSensitive ? StringComparer.InvariantCulture : StringComparer.CurrentCultureIgnoreCase);

         foreach (string arg in NormalizeArguments(args))
         {
            if (!string.IsNullOrEmpty(arg))
            {
               if (IsNamedParameter(arg))
                  ParseNamedParameter(arg, arguments);
               else
                  ParseOption(arg, arguments);
            }
         }

         return arguments;
      }

      /// <summary>Parses the specified arguments.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments to parse.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>A <see cref="IDictionary{TKey,TValue}"/> the arguments were parsed into</returns>
      public IDictionary<string, string> Parse<T>(string[] args, bool caseSensitive)
      {
         var normalizedArguments = new Dictionary<string, string>();

         var arguments = Parse(args, caseSensitive);

         var translateToNormalizedName = CreateArgumentTranslationMap<T>(caseSensitive);

         foreach (var argument in arguments)
         {
            if (translateToNormalizedName.ContainsKey(argument.Key))
            {
               normalizedArguments.Add(translateToNormalizedName[argument.Key], argument.Value);
            }
            else
            {
               throw new CommandLineArgumentException($"The argument {argument.Key} is not defined!");
            }
         }

         return normalizedArguments;
      }

      /// <summary>Parses the specified arguments.</summary>
      /// <param name="args">The arguments to parse.</param>
      /// <returns>A <see cref="IDictionary{TKey,TValue}"/> the arguments were parsed into</returns>
      public IDictionary<string, string> Parse(string[] args)
      {
         return Parse(args, false);
      }

      /// <summary>Converts the given arguments instance class to an argument string.</summary>
      /// <typeparam name="T">The type of the arguments class</typeparam>
      /// <param name="instance">The instance.</param>
      /// <returns>The command line argument string</returns>
      public string UnMap<T>(T instance)
      {
         var mapper = new ArgumentMapper<T>();
         return mapper.UnMap(instance);
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

            if (candidate.Length == 1 && NameSeparators.Contains(candidate[0]))
            {
               normalized.Add($"{current}{candidate}{next}");
               i += 2;
            }
            else
            {
               normalized.Add(current);
            }
         }

         return normalized;
      }

      private static IDictionary<string, string> CreateArgumentTranslationMap<T>(bool caseSensitive)
      {
         var translateToNormalizedName = new Dictionary<string, string>();

         foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
         {
            var commandLineAttributeList = propertyInfo.GetCustomAttributes(typeof(CommandLineAttribute), true).Cast<CommandLineAttribute>();

            foreach (var commandLineAttribute in commandLineAttributeList)
            {
               var name = commandLineAttribute.Name ?? propertyInfo.Name;

               if (!caseSensitive)
                  name = name.ToLowerInvariant();

               translateToNormalizedName.Add(name, name);

               foreach (var alias in commandLineAttribute.Aliases)
               {
                  var key = caseSensitive ? alias : alias.ToLowerInvariant();

                  if (translateToNormalizedName.ContainsKey(key))
                  {
                     string message = $"The argument {key} is allready defined as {translateToNormalizedName[key]}!";
                     throw new CommandLineArgumentException(message);
                  }

                  translateToNormalizedName.Add(key, name);
               }
            }
         }

         return translateToNormalizedName;
      }

      private static string[] GetAliases(ArgumentAttribute argumentAttribute, OptionAttribute optionAttribute, CommandAttribute commandAttribute)
      {
         if (argumentAttribute != null)
            return argumentAttribute.Aliases;

         if (optionAttribute != null)
            return optionAttribute.Aliases;

         if (commandAttribute != null)
            return commandAttribute.Aliases;

         return new string[0];
      }

      private static string GetArgumentName(PropertyInfo info, ArgumentAttribute argumentAttribute, OptionAttribute optionAttribute, CommandAttribute commandAttribute)
      {
         string primaryName = info.Name;
         if (argumentAttribute?.Name != null)
            return argumentAttribute.Name.ToLowerInvariant();

         if (optionAttribute?.Name != null)
            return optionAttribute.Name.ToLowerInvariant();

         if (commandAttribute?.Name != null)
            return commandAttribute.Name.ToLowerInvariant();

         return primaryName.ToLowerInvariant();
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

      private static IEnumerable<string> GetWrappedStrings(string text, int maxLength)
      {
         if (text.Length < maxLength)
         {
            yield return text;
            yield break;
         }

         StringBuilder builder = new StringBuilder();
         foreach (var word in text.Split(' '))
         {
            var candidate = builder.ToString();

            builder.Append(word);
            builder.Append(" ");

            if (builder.Length > maxLength)
            {
               builder = new StringBuilder(word);
               builder.Append(" ");

               yield return candidate.TrimEnd();
            }
         }

         yield return builder.ToString();
      }

      private static bool IsNamedParameter(string argumentString)
      {
         return NameSeparators.Any(argumentString.Contains);
      }

      private static void ParseNamedParameter(string argumentString, IDictionary<string, string> arguments)
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

         arguments[name] = valueString;
      }

      private static void ParseOption(string argumentString, IDictionary<string, string> arguments)
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

         arguments.Add(option, "true");
      }

      private static string[] Split(string argumentString)
      {
         int index = GetSplitIndex(argumentString);
         var name = argumentString.Substring(0, index);
         var valueStart = index + 1;
         var value = valueStart >= argumentString.Length ? string.Empty : argumentString.Substring(valueStart);
         return new[] { name, value };
      }

      #endregion
   }
}