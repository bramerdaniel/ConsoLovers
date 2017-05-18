// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultHelpTextProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
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

   using JetBrains.Annotations;

   internal class DefaultHelpTextProvider : IHelpTextProvider
   {
      #region Constants and Fields

      private Type type;

      private ResourceManager resourceManager;

      #endregion

      #region IHelpTextProvider Members

      public void WriteHeader()
      {
         if (type.IsCommandType())
         {
            Console.WriteLine("Help for the command");
            Console.WriteLine();
            return;
         }

         if (type.IsClass)
         {
            Console.WriteLine("Help for the command line arguments that are supported");
            Console.WriteLine();
         }


      }

      public void WriteNoHelpAvailable()
      {
         Console.WriteLine("No HelpTextAttributes found for help generation.");
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

      /// <summary>Gets the help information for the class of the given type.</summary>
      /// <param name="argumentType">The argument class for creating the help for</param>
      /// <param name="resourceManager">The resource manager that will be used for localization</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      public IEnumerable<ArgumentHelp> GetHelpForProperties([NotNull] Type argumentType, ResourceManager resourceManager)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         PropertyInfo[] properties = argumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
         foreach (PropertyInfo info in properties)
         {
            var argumentAttribute = (ArgumentAttribute)info.GetCustomAttributes(typeof(ArgumentAttribute), true).FirstOrDefault();
            var optionAttribute = (OptionAttribute)info.GetCustomAttributes(typeof(OptionAttribute), true).FirstOrDefault();
            var commandAttribute = (CommandAttribute)info.GetCustomAttributes(typeof(CommandAttribute), true).FirstOrDefault();
            var helpText = (HelpTextAttribute)info.GetCustomAttributes(typeof(HelpTextAttribute), true).FirstOrDefault();
            if (helpText != null)
            {
               yield return new ArgumentHelp
               {
                  PropertyName = GetArgumentName(info, argumentAttribute, optionAttribute, commandAttribute),
                  Aliases = GetAliases(argumentAttribute, optionAttribute, commandAttribute),
                  UnlocalizedDescription = helpText.Description,
                  LocalizedDescription = resourceManager?.GetString(helpText.ResourceKey)
               };
            }
         }
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

      private static int GetConsoleWidth()
      {
         try
         {
            return Console.WindowWidth;
         }
         catch
         {
            return 140;
         }
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


      public void WriteArguments()
      {
         var consoleWidth = GetConsoleWidth();

         var argumentHelps = GetHelpForProperties(type, resourceManager).ToList();
         if (argumentHelps.Count == 0)
         {
            Console.WriteLine("No HelpTextAttributes found for help generation.");
            return;
         }

         int longestNameWidth = argumentHelps.Select(a => a.PropertyName.Length).Max() + 2;
         int longestAliasWidth = argumentHelps.Select(a => a.AliasString.Length).Max() + 4;

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
               Console.WriteLine(" ".PadLeft(leftWidth) + part);
         }
      }

      public void Initialize(Type helpType, ResourceManager resourceManager)
      {
         type = helpType;
         this.resourceManager = resourceManager;
      }

      public void WriteFooter()
      {
      }

      #endregion
   }
}