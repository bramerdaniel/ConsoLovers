// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineEngine.cs" company="ConsoLovers">
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

   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   /// <summary>Main class that us is used for processing command line arguments</summary>
   public class CommandLineEngine : ICommandLineEngine
   {
      #region Constructors and Destructors

      [InjectionConstructor]
      public CommandLineEngine([NotNull] IObjectFactory engineFactory)
      {
         if (engineFactory == null)
            throw new ArgumentNullException(nameof(engineFactory));

         EngineFactory = engineFactory;
      }

      public CommandLineEngine()
         : this(new DefaultFactory())
      {
      }

      #endregion

      #region ICommandLineEngine Members

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
         return GetHelpForProperties(typeof(T), resourceManager);
      }

      /// <summary>Maps the specified arguments to a class of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args)
         where T : class
      {
         return Map<T>(args, false);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="caseSensitive">if set to <c>true</c> the parameters are treated case sensitive.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args, bool caseSensitive)
         where T : class
      {
         var arguments = ArgumentParser.ParseArguments(args, caseSensitive);
         var mapper = CreateMapper<T>();
         return mapper.Map(arguments);
      }

      /// <summary>Maps the specified arguments to given object of the given type.</summary>
      /// <typeparam name="T">The type of the class to map the argument to.</typeparam>
      /// <param name="args">The arguments that should be mapped to the instance.</param>
      /// <param name="instance">The instance of <see cref="T"/> the args should be mapped to.</param>
      /// <returns>The created instance of the arguments class.</returns>
      public T Map<T>(string[] args, T instance)
         where T : class
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
         where T : class
      {
         var arguments = ArgumentParser.ParseArguments(args, caseSensitive);
         var mapper = CreateMapper<T>();

         return mapper.Map(arguments, instance);
      }

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for </typeparam>
      /// <param name="resourceManager">The resource manager that will be used for localization.</param>
      public void PrintHelp<T>(ResourceManager resourceManager)
      {
         PrintHelp(typeof(T), resourceManager);
      }

      public void PrintHelp([NotNull] Type type, ResourceManager resourceManager)
      {
         var helpTextProvider = GetHelpTextProvider(type, resourceManager);
         helpTextProvider.PrintHelp();
      }

      public void PrintHelp(PropertyInfo propertyInfo, ResourceManager resourceManager)
      {
         var helpTextProvider = GetHelpTextProvider(propertyInfo, resourceManager);
         helpTextProvider.PrintHelp();
      }

      #endregion

      #region Public Properties

      public CommandLineArgumentParser ArgumentParser { get; set; } = new CommandLineArgumentParser();

      #endregion

      #region Properties

      /// <summary>Gets the mapper factory.</summary>
      internal IObjectFactory EngineFactory { get; set; }

      #endregion

      #region Public Methods and Operators

      public string GetHelpForClass([NotNull] Type argumentType, ResourceManager resourceManager)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         var helpText = (HelpTextAttribute)argumentType.GetCustomAttributes(typeof(HelpTextAttribute), true).FirstOrDefault();
         if (helpText != null)
         {
            if (resourceManager == null)
               return helpText.Description;
            resourceManager.GetString(helpText.ResourceKey);
         }

         return null;
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

      private IArgumentMapper<T> CreateMapper<T>()
         where T : class
      {
         var info = new ArgumentClassInfo(typeof(T));
         return info.HasCommands ? (IArgumentMapper<T>)EngineFactory.CreateInstance<CommandMapper<T>>() : EngineFactory.CreateInstance<ArgumentMapper<T>>();
      }

      private IHelpProvider GetHelpTextProvider(Type argumentType, ResourceManager resourceManager)
      {
         var providerType = (argumentType.GetCustomAttribute(typeof(HelpTextProviderAttribute)) as HelpTextProviderAttribute)?.Type;
         if (providerType != null)
         {
            var provider = EngineFactory.CreateInstance(providerType) as IHelpProvider;
            if (provider != null)
               return provider;
         }

         return new TypeHelpProvider(argumentType, resourceManager);
      }

      private IHelpProvider GetHelpTextProvider(PropertyInfo propertyInfo, ResourceManager resourceManager)
      {
         var providerType = (propertyInfo.GetCustomAttribute(typeof(HelpTextProviderAttribute)) as HelpTextProviderAttribute)?.Type;
         if (providerType != null)
         {
            var provider = EngineFactory.CreateInstance(providerType) as IHelpProvider;
            if (provider != null)
               return provider;
         }

         return new PropertyHelpProvider(propertyInfo, resourceManager);
      }

      #endregion
   }
}