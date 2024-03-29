﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentClassInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>The <see cref="ArgumentClassInfo"/> is a helper class, that is able to analyze the class representing the command line arguments. This is done by reflection</summary>
   [DebuggerDisplay("{ArgumentType?.Name}")]
   public class ArgumentClassInfo
   {
      #region Constants and Fields

      private List<CommandInfo> commandInfos;

      private List<ParameterInfo> properties;

      #endregion

      #region Constructors and Destructors

      internal ArgumentClassInfo([NotNull] Type argumentType)
      {
         ArgumentType = argumentType ?? throw new ArgumentNullException(nameof(argumentType));
         Initialize();
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the type of the class containing the argument definitions.</summary>
      public Type ArgumentType { get; }

      public IReadOnlyCollection<CommandInfo> CommandInfos => commandInfos;

      /// <summary>Gets the default command.</summary>
      public CommandInfo DefaultCommand { get; private set; }

      public bool HasCommands => CommandInfos.Any();

      public CommandInfo HelpCommand { get; private set; }

      public IReadOnlyCollection<ParameterInfo> Properties => properties;

      #endregion

      #region Public Methods and Operators

      public ParameterInfo GetParameterInfo(string name)
      {
         foreach (var parmeterInfo in Properties)
         {
            if (parmeterInfo.Identifiers.Any(identifier => string.Equals(name, identifier, StringComparison.InvariantCultureIgnoreCase)))
               return parmeterInfo;
         }

         return null;
      }

      #endregion

      #region Methods

      private static ParameterInfo CreateInfo(PropertyInfo propertyInfo, CommandLineAttribute attribute)
      {
         if (attribute is CommandAttribute commandAttribute)
            return new CommandInfo(propertyInfo, commandAttribute);

         if (attribute is ArgumentAttribute argumentAttribute)
            return new ArgumentInfo(propertyInfo, argumentAttribute);

         if (attribute is OptionAttribute optionAttribute)
            return new OptionInfo(propertyInfo, optionAttribute);

         return null;
      }

      private void Initialize()
      {
         commandInfos = new List<CommandInfo>();
         properties = new List<ParameterInfo>();

         foreach (var kvp in ArgumentType.GetPropertiesWithAttributes())
         {
            var propertyInfo = kvp.Key;
            var attribute = kvp.Value;

            var parameterInfo = CreateInfo(propertyInfo, attribute);
            properties.Add(parameterInfo);

            if (parameterInfo is CommandInfo commandInfo)
            {
               commandInfos.Add(commandInfo);

               SetAsHelpCommandWhenRequired(propertyInfo, commandInfo);
               SetAsDefaultCommandWhenSpecified(commandInfo);
            }
         }
      }

      private void SetAsHelpCommandWhenRequired(PropertyInfo propertyInfo, CommandInfo commandInfo)
      {
         if (propertyInfo.PropertyType == typeof(HelpCommand))
            HelpCommand = commandInfo;
      }

      private void SetAsDefaultCommandWhenSpecified(CommandInfo commandInfo)
      {
         if (commandInfo.IsDefault)
         {
            if (DefaultCommand != null)
               throw new InvalidOperationException("Default command was defined twice.");

            DefaultCommand = commandInfo;
         }
      }

      #endregion
   }
}