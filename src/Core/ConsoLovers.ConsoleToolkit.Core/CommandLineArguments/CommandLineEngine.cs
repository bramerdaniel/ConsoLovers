// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineEngine.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   using ServiceProviderServiceExtensions = ConsoLovers.ConsoleToolkit.Core.ServiceProviderServiceExtensions;

   /// <summary>Main class that us is used for processing command line arguments</summary>
   public class CommandLineEngine : ICommandLineEngine
   {
      #region Constructors and Destructors

      [InjectionConstructor]
      public CommandLineEngine([NotNull] IServiceProvider serviceProvider, [NotNull] IExecutionEngine executionEngine,
         [NotNull] ICommandLineArgumentParser argumentParser, [NotNull] IArgumentReflector argumentReflector)
      {
         ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         ArgumentParser = argumentParser ?? throw new ArgumentNullException(nameof(argumentParser));
         ArgumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
      }

      #endregion

      #region Public Events

      /// <summary>
      ///    Occurs when command line argument was passed to the <see cref="T:ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ICommandLineEngine"/>
      ///    and it was processed and mapped to a specific property.
      /// </summary>
      public event EventHandler<CommandLineArgumentEventArgs> HandledCommandLineArgument;

      /// <summary>Occurs when command line argument was passed to the <see cref="ICommandLineEngine"/> the could not be processed in any way.</summary>
      public event EventHandler<CommandLineArgumentEventArgs> UnhandledCommandLineArgument;

      #endregion

      #region ICommandLineEngine Members

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for</typeparam>
      /// <param name="localizationService">The <see cref="ILocalizationService"/> that will be used for localization.</param>
      /// <param name="consoleWidth">Width of the console.</param>
      /// <returns>A <see cref="StringBuilder"/> containing the formatted help text.</returns>
      public StringBuilder FormatHelp<T>(ILocalizationService localizationService, int consoleWidth)
      {
         var stringBuilder = new StringBuilder();
         var argumentHelps = GetHelp<T>(localizationService).ToList();
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
      /// <param name="localizationService">The <see cref="ILocalizationService"/> that will be used for localization</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      public IEnumerable<ArgumentHelp> GetHelp<T>(ILocalizationService localizationService)
      {
         return GetHelpForProperties(typeof(T), localizationService);
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

         try
         {
            mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
            mapper.MappedCommandLineArgument += OnMappedCommandLineArgument;
            return mapper.Map(arguments);
         }
         finally
         {
            mapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
            mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
         }
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

      public T Map<T>(string args, T instance)
         where T : class
      {
         return Map(args, instance, false);
      }

      public T Map<T>(string args)
         where T : class
      {
         return Map<T>(args, false);
      }

      public T Map<T>(string args, bool caseSensitive)
         where T : class
      {
         var arguments = ArgumentParser.ParseArguments(args, caseSensitive);
         var mapper = CreateMapper<T>();

         try
         {
            mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
            mapper.MappedCommandLineArgument += OnMappedCommandLineArgument;
            return mapper.Map(arguments);
         }
         finally
         {
            mapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
            mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
         }
      }

      public T Map<T>(string args, T instance, bool caseSensitive)
         where T : class
      {
         var arguments = ArgumentParser.ParseArguments(args, caseSensitive);
         var mapper = CreateMapper<T>();

         try
         {
            mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
            mapper.MappedCommandLineArgument += OnMappedCommandLineArgument;
            return mapper.Map(arguments, instance);
         }
         finally
         {
            mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
            mapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
         }
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

         try
         {
            mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
            mapper.MappedCommandLineArgument += OnMappedCommandLineArgument;
            return mapper.Map(arguments, instance);
         }
         finally
         {
            mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
            mapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
         }
      }

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <typeparam name="T">Type of the argument class to print the help for </typeparam>
      /// <param name="localizationService">The <see cref="ILocalizationService"/> that will be used for localization.</param>
      public void PrintHelp<T>(ILocalizationService localizationService)
      {
         PrintHelp(typeof(T), localizationService);
      }

      /// <summary>Prints the help to the <see cref="Console"/>.</summary>
      /// <param name="argumentType">Type of the argument class to print the help for</param>
      /// <param name="localizationService">The <see cref="ILocalizationService"/> that will be used for localization.</param>
      public void PrintHelp([NotNull] Type argumentType, ILocalizationService localizationService)
      {
         var helpTextProvider = GetHelpTextProvider(argumentType, localizationService);
         helpTextProvider.PrintTypeHelp(argumentType);
      }

      /// <summary>Prints the help for the given <see cref="!:propertyInfo"/> to the <see cref="T:System.Console"/>.</summary>
      /// <param name="propertyInfo">The <see cref="T:System.Reflection.PropertyInfo"/> to print the help for</param>
      /// <param name="localizationService">The <see cref="ILocalizationService"/> that will be used for localization.</param>
      public void PrintHelp(PropertyInfo propertyInfo, ILocalizationService localizationService)
      {
         var helpTextProvider = GetHelpTextProvider(propertyInfo, localizationService);
         helpTextProvider.PrintPropertyHelp(propertyInfo);
      }

      #endregion

      #region Public Properties

      public IArgumentReflector ArgumentReflector { get; }

      #endregion

      #region Properties

      /// <summary>Gets the <see cref="ICommandLineArgumentParser"/> that will be used.</summary>
      internal ICommandLineArgumentParser ArgumentParser { get; }

      /// <summary>Gets the service provider.</summary>
      internal IServiceProvider ServiceProvider { get; }

      #endregion

      #region Public Methods and Operators

      public string GetHelpForClass([NotNull] Type argumentType, ILocalizationService localizationService)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         var helpText = argumentType.GetCustomAttribute<HelpTextAttribute>(true);
         if (helpText != null)
         {
            if (localizationService == null)
            {
               if (string.IsNullOrEmpty(helpText.Description))
                  return helpText.ResourceKey ?? "NoResourceKeyOrDescription";

               return helpText.Description;
            }

            GetLocalizedDescription(localizationService, helpText.ResourceKey);
         }

         return null;
      }

      /// <summary>Gets the help information for the class of the given type.</summary>
      /// <param name="argumentType">The argument class for creating the help for</param>
      /// <param name="localizationService">The resource manager that will be used for localization</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ArgumentHelp"/></returns>
      public IEnumerable<ArgumentHelp> GetHelpForProperties([NotNull] Type argumentType, ILocalizationService localizationService)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         PropertyInfo[] properties = argumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
         foreach (PropertyInfo info in properties)
         {
            var argumentAttribute = info.GetCustomAttribute<ArgumentAttribute>(true);
            var optionAttribute = info.GetCustomAttribute<OptionAttribute>(true);
            var commandAttribute = info.GetCustomAttribute<CommandAttribute>(true);
            var helpText = info.GetCustomAttribute<HelpTextAttribute>(true);
            if (helpText != null)
            {
               yield return new ArgumentHelp
               {
                  PropertyName = GetArgumentName(info, argumentAttribute, optionAttribute, commandAttribute),
                  Aliases = GetAliases(argumentAttribute, optionAttribute, commandAttribute),
                  UnlocalizedDescription = helpText.Description,
                  LocalizedDescription = GetLocalizedDescription(localizationService, helpText.ResourceKey)
               };
            }
         }
      }

      #endregion

      #region Methods

      internal static string GetLocalizedDescription(ILocalizationService resourceManager, string resourceKey)
      {
         if (resourceManager == null || resourceKey == null)
            return null;

         return resourceManager.GetLocalizedSting(resourceKey);
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

      private static string GetArgumentName(PropertyInfo info, ArgumentAttribute argumentAttribute, OptionAttribute optionAttribute,
         CommandAttribute commandAttribute)
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
         var info = ArgumentReflector.GetTypeInfo<T>();
         return info.HasCommands
            ? ActivatorUtilities.GetServiceOrCreateInstance<CommandMapper<T>>(ServiceProvider)
            : ActivatorUtilities.GetServiceOrCreateInstance<ArgumentMapper<T>>(ServiceProvider);
      }

      private IHelpProvider GetHelpTextProvider(Type argumentType, ILocalizationService resourceManager)
      {
         var providerType = argumentType.GetCustomAttribute<HelpTextProviderAttribute>()?.Type;
         if (providerType != null && ServiceProvider.GetService(providerType) is IHelpProvider provider)
            return provider;

         return new TypeHelpProvider(ServiceProvider, resourceManager);
      }

      private IHelpProvider GetHelpTextProvider(PropertyInfo propertyInfo, ILocalizationService localizationService)
      {
         var propertyDeclaringType = propertyInfo.DeclaringType?.GetCustomAttribute<HelpTextProviderAttribute>()?.Type;
         if (propertyDeclaringType != null
             && ServiceProviderServiceExtensions.GetRequiredService(ServiceProvider, propertyDeclaringType) is IHelpProvider provider)
            return provider;

         return new PropertyHelpProvider(localizationService);
      }

      private void OnMappedCommandLineArgument(object sender, MapperEventArgs e)
      {
         HandledCommandLineArgument?.Invoke(this, new CommandLineArgumentEventArgs(e.Argument, e.PropertyInfo, e.Instance));
      }

      private void OnUnmappedCommandLineArgument(object sender, MapperEventArgs e)
      {
         UnhandledCommandLineArgument?.Invoke(this, new CommandLineArgumentEventArgs(e.Argument, e.PropertyInfo, e.Instance));
      }

      #endregion
   }
}