// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   public class TypeHelpProvider : IHelpProvider
   {
      public IServiceProvider ServiceProvider { get; }

      #region Constants and Fields

      private readonly ILocalizationService localizationService;

      private IConsole console;

      #endregion

      #region Constructors and Destructors

      [InjectionConstructor]
      public TypeHelpProvider([NotNull] IServiceProvider serviceProvider, [NotNull] ILocalizationService localizationService)
      {
         ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
      }

      #endregion

      #region IHelpProvider Members

      /// <summary>Prints the help.</summary>
      /// <param name="type">Type of the argument.</param>
      public void PrintTypeHelp(Type type)
      {
         var helpRequest = new TypeHelpRequest(type);

         WriteTypeHeader(helpRequest);
         WriteTypeContent(helpRequest);
         WriteTypeFooter(helpRequest);
      }

      /// <summary>Prints the help for the given property.</summary>
      /// <param name="property">The argument property.</param>
      public void PrintPropertyHelp([NotNull] PropertyInfo property)
      {
         if (property == null)
            throw new ArgumentNullException(nameof(property));

         var helpRequest = new PropertyHelpRequest(property);

         WritePropertyHeader(helpRequest);
         WritePropertyContent(helpRequest);
         WritePropertyFooter(helpRequest);
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

         foreach (var propAndAttribute in argumentType.GetPropertiesWithAttributes())
         {
            var propertyInfo = propAndAttribute.Key;
            var commandLineAttribute = propAndAttribute.Value;

            var helpText = propertyInfo.GetAttribute<HelpTextAttribute>();
            if (helpText != null)
            {
               yield return new ArgumentHelp
               {
                  PropertyName = GetArgumentName(propertyInfo, commandLineAttribute),
                  Aliases = GetAliases(commandLineAttribute),
                  UnlocalizedDescription = helpText.Description,
                  LocalizedDescription = helpText.ResourceKey != null ? localizationService?.GetLocalizedSting(helpText.ResourceKey) : null,
                  Priority = helpText.Priority,
                  Required = IsRequired(commandLineAttribute)
               };
            }
         }
      }

      public virtual void WriteTypeContent(TypeHelpRequest helpRequest)
      {
         var consoleWidth = GetConsoleWidth();

         var argumentHelps = GetHelpForProperties(helpRequest.Type).OrderByDescending(x => x.Priority).ToList();
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

      /// <summary>Writes the footer for a type help request.</summary>
      /// <param name="helpRequest"></param>
      public virtual void WriteTypeFooter(TypeHelpRequest helpRequest)
      {
         if (helpRequest.IsCustomFooter() && ServiceProvider.GetService(helpRequest.Type) is ICustomizedFooter customizedFooter)
         {
            customizedFooter.WriteFooter(Console);
         }
         else
         {
            WriteSeparator();
         }
      }

      public virtual void WriteTypeHeader(TypeHelpRequest helpRequest)
      {
         if (helpRequest.IsCustomHeader() && ServiceProvider.GetService(helpRequest.Type) is ICustomizedHeader customizedHeader)
         {
            customizedHeader.WriteHeader(Console);
            return;
         }

         if (helpRequest.Type.IsCommandType())
         {
            Console.WriteLine($"Help for the '{GetCommandName(helpRequest.Type)}' command");
            Console.WriteLine();
            return;
         }

         if (helpRequest.Type.IsClass)
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

      protected virtual ConsoleColor GetNameForeground(ArgumentHelp info)
      {
         return info.Required ? ConsoleColor.Red : Console.ForegroundColor;
      }

      protected virtual void OnNoPropertyHelpAvailable()
      {
         Console.WriteLine("No help for the arguments available");
      }

      protected virtual void WriteHelpText(string resourceKey, string description)
      {
         if (localizationService != null && !string.IsNullOrEmpty(resourceKey))
         {
            var helpTextString = localizationService.GetLocalizedSting(resourceKey);
            Console.WriteLine($"- {helpTextString}");
         }
         else
         {
            if (description != null)
               Console.WriteLine($"- {description}");
         }
      }

      protected virtual void WriteNoHelpTextAvailable(PropertyHelpRequest property)
      {
      }

      /// <summary>Writes the footer for a property help request.</summary>
      /// <param name="property">The property the help was requested for.</param>
      protected virtual void WritePropertyFooter(PropertyHelpRequest property)
      {
      }

      /// <summary>Writes the header for a property help request.</summary>
      /// <param name="helpRequest">The property.</param>
      protected virtual void WritePropertyHeader(PropertyHelpRequest helpRequest)
      {
         Console.WriteLine($"Help for the {GetCommandLineTyp(helpRequest)} '{GetArgumentName(helpRequest)}'");
         Console.WriteLine();
      }

      protected void WriteSeparator(char paddingChar = '-')
      {
         Console.WriteLine(string.Empty.PadRight(GetConsoleWidth(), paddingChar));
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

      private string GetArgumentName(PropertyHelpRequest request)
      {
         string primaryName = request.Property.Name;

         var attribute = request.CommandLineAttribute;
         if (attribute?.Name != null)
            return attribute.Name.ToLowerInvariant();

         return primaryName.ToLowerInvariant();
      }

      private string GetCommandLineTyp(PropertyHelpRequest helpRequest)
      {
         if (helpRequest.CommandLineAttribute is OptionAttribute)
            return "option";
         if (helpRequest.CommandLineAttribute is ArgumentAttribute)
            return "argument";
         if (helpRequest.CommandLineAttribute is CommandAttribute)
            return "command";
         return "property";
      }

      private string GetCommandName(Type type)
      {
         return type.Name;
      }

      private bool IsRequired(CommandLineAttribute commandLineAttribute)
      {
         return commandLineAttribute is ArgumentAttribute attribute && attribute.Required;
      }

      private void WriteArgument(ArgumentHelp argumentHelp, int longestNameWidth, int longestAliasWidth, int descriptionWidth, int leftWidth)
      {
         var name = $"-{argumentHelp.PropertyName}".PadRight(longestNameWidth);
         var aliasString = ComputeAliasString(argumentHelp, longestAliasWidth);


         Console.Write(name, GetNameForeground(argumentHelp));
         Console.Write(aliasString);

         var descriptionLines = GetWrappedStrings(argumentHelp.Description, descriptionWidth).ToList();

         Console.WriteLine(descriptionLines[0]);
         foreach (var part in descriptionLines.Skip(1))
            Console.WriteLine(" ".PadLeft(leftWidth) + part);
      }

      private static string ComputeAliasString(ArgumentHelp argumentHelp, int width)
      {
         if (string.IsNullOrWhiteSpace(argumentHelp.AliasString))
            return "".PadRight(width);
         return $"[{argumentHelp.AliasString}]".PadRight(width);
      }

      private void WritePropertyContent(PropertyHelpRequest property)
      {
         if (property.DetailedHelpTextAttribute != null)
         {
            WriteHelpText(property.DetailedHelpTextAttribute.ResourceKey, property.DetailedHelpTextAttribute.Description);
         }
         else if (property.HelpTextAttribute != null)
         {
            WriteHelpText(property.HelpTextAttribute.ResourceKey, property.HelpTextAttribute.Description);
         }
         else
         {
            WriteNoHelpTextAvailable(property);
         }
      }

      #endregion
   }
}