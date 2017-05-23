// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProvider.cs" company="ConsoLovers">
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

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   public class TypeHelpProvider : IHelpProvider
   {
      #region Constants and Fields

      private readonly ResourceManager resourceManager;

      private readonly Type type;

      private IConsole console;

      #endregion

      #region Constructors and Destructors

      public TypeHelpProvider([NotNull] Type type)
         : this(type, null)
      {
      }

      [InjectionConstructor]
      public TypeHelpProvider([NotNull] Type type, [CanBeNull] ResourceManager resourceManager)
      {
         if (type == null)
            throw new ArgumentNullException(nameof(type));

         this.type = type;
         this.resourceManager = resourceManager;
      }

      #endregion

      #region IHelpProvider Members

      public void PrintHelp()
      {
         WriteHeader();
         WriteContent();
         WriteFooter();
      }

      #endregion

      #region Properties

      /// <summary>Gets the console that should be used.</summary>
      protected IConsole Console => console ?? (console = CreateConsole());

      #endregion

      #region Public Methods and Operators

      /// <summary>Gets the help information for the class of the given type.</summary>
      /// <param name="argumentType">The argument class for creating the help for</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      public IEnumerable<ArgumentHelp> GetHelpForProperties([NotNull] Type argumentType)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         PropertyInfo[] properties = argumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
         foreach (PropertyInfo info in properties)
         {
            var commandLineAttribute = info.GetAttribute<CommandLineAttribute>();
            var helpText = info.GetAttribute<HelpTextAttribute>();
            if (helpText != null)
            {
               yield return new ArgumentHelp
               {
                  PropertyName = GetArgumentName(info, commandLineAttribute),
                  Aliases = GetAliases(commandLineAttribute),
                  UnlocalizedDescription = helpText.Description,
                  LocalizedDescription = resourceManager?.GetString(helpText.ResourceKey),
                  Priority = helpText.Priority,
                  Required = IsRequired(commandLineAttribute)
               };
            }
         }
      }

      public virtual void WriteContent()
      {
         var consoleWidth = GetConsoleWidth();

         var argumentHelps = GetHelpForProperties(type).OrderByDescending(x => x.Priority).ToList();
         if (argumentHelps.Count == 0)
         {
            OnNoPropertyHelpAvailable();
            return;
         }

         int longestNameWidth = argumentHelps.Select(a => a.PropertyName.Length).Max() + 2;
         int longestAliasWidth = argumentHelps.Select(a => a.AliasString.Length).Max() + 4;

         int descriptionWidth = consoleWidth - longestNameWidth - longestAliasWidth;
         int leftWidth = consoleWidth - descriptionWidth;

         foreach (ArgumentHelp argumentHelp in argumentHelps)
         {
            BeforerArgument(argumentHelp);
            WriteArgument(argumentHelp, longestNameWidth, longestAliasWidth, descriptionWidth, leftWidth);
            AfterArgument(argumentHelp);
         }
      }

      public virtual void WriteFooter()
      {
      }

      public virtual void WriteHeader()
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

      #endregion

      #region Methods

      protected virtual void AfterArgument(ArgumentHelp help)
      {
      }

      protected virtual void BeforerArgument(ArgumentHelp help)
      {
      }

      protected virtual IConsole CreateConsole()
      {
         return new ConsoleProxy();
      }

      protected int GetConsoleWidth()
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

      protected virtual void OnNoPropertyHelpAvailable()
      {
         Console.WriteLine("No help for the arguments available");
      }

      private static string[] GetAliases(CommandLineAttribute attribute)
      {
         if (attribute != null)
            return attribute.Aliases;

         return new string[0];
      }

      private static string GetArgumentName(PropertyInfo info, CommandLineAttribute commandLineAttribute)
      {
         string primaryName = info.Name;

         if (commandLineAttribute?.Name != null)
            return commandLineAttribute.Name.ToLowerInvariant();

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

      protected virtual ConsoleColor GetNameForeground(ArgumentHelp info)
      {
         return info.Required ? ConsoleColor.Red : Console.ForegroundColor;
      }

      private bool IsRequired(CommandLineAttribute commandLineAttribute)
      {
         var attribute = commandLineAttribute as ArgumentAttribute;
         return attribute != null && attribute.Required;
      }

      private void WriteArgument(ArgumentHelp argumentHelp, int longestNameWidth, int longestAliasWidth, int descriptionWidth, int leftWidth)
      {
         var name = $"-{argumentHelp.PropertyName}".PadRight(longestNameWidth);
         var aliasString = $"[{argumentHelp.AliasString}]".PadRight(longestAliasWidth);

         Console.Write(name, GetNameForeground(argumentHelp));
         Console.Write(aliasString);

         var descriptionLines = GetWrappedStrings(argumentHelp.Description, descriptionWidth).ToList();

         Console.WriteLine(descriptionLines[0]);
         foreach (var part in descriptionLines.Skip(1))
            Console.WriteLine(" ".PadLeft(leftWidth) + part);
      }

      #endregion
   }
}