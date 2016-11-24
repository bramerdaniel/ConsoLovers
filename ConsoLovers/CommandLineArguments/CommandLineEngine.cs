// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineEngine.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Resources;
   using System.Text;

   /// <summary>Main class that us is used for processing command line arguments</summary>
   public class CommandLineEngine : ICommandLineEngine
   {
      #region Public Properties

      public CommandLineArgumentParser ArgumentParser { get; set; } = new CommandLineArgumentParser();

      #endregion

      #region Public Methods and Operators

      /// <summary>Prints the help to the <see cref="Console" />.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for</typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      /// <param name="consoleWidth">Width of the console.</param>
      /// <returns>A <see cref="StringBuilder" /> containing the formatted help text.</returns>
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
         var arguments = ArgumentParser.ParseArguments(args, caseSensitive);
         var mapper = new ArgumentMapper<T>();

         return mapper.Map(arguments);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args, T instance)
      {
         return Map(args, instance, false);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args, T instance, bool caseSensitive)
      {
         var arguments = ArgumentParser.ParseArguments(args, caseSensitive);
         var mapper = new ArgumentMapper<T>();

         return mapper.Map(arguments, instance);
      }

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for </typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      public void PrintHelp<T>(ResourceManager resourceManager)
      {
         var argumentHelps = GetHelp<T>(resourceManager).ToList();
         if (argumentHelps.Count == 0)
         {
            Console.WriteLine("No HelpTextAttributes found for help generation.");
            return;
         }

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

      #endregion

      #region Methods

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

      #endregion
   }

   public interface ICommandLineEngine
   {
      /// <summary>Gets the help information for the class of the given type.</summary>
      /// <typeparam name="T">The argument class for creating the help for</typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      IEnumerable<ArgumentHelp> GetHelp<T>(ResourceManager resourceManager);

      /// <summary>Maps the specified arguments to a class of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string[] args);

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string[] args, bool caseSensitive);

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string[] args, T instance);

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>The created instance of the arguments class.</returns>
      T Map<T>(string[] args, T instance, bool caseSensitive);

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for </typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      void PrintHelp<T>(ResourceManager resourceManager);

      /// <summary>Prints the help to the <see cref="Console" />.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for</typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      /// <param name="consoleWidth">Width of the console.</param>
      /// <returns>A <see cref="StringBuilder" /> containing the formatted help text.</returns>
      StringBuilder FormatHelp<T>(ResourceManager resourceManager, int consoleWidth);
   }
}