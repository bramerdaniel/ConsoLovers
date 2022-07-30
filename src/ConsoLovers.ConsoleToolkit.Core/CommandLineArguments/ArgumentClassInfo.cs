// --------------------------------------------------------------------------------------------------------------------
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

      private ArgumentClassInfo([NotNull] Type argumentType)
      {
         ArgumentType = argumentType ?? throw new ArgumentNullException(nameof(argumentType));
         Initialize();
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the type of the class containing the argument definitions.</summary>
      public Type ArgumentType { get; }

      public IReadOnlyCollection<CommandInfo> CommandInfos => commandInfos;

      public CommandInfo DefaultCommand { get; private set; }

      public bool HasCommands => CommandInfos.Any();

      public CommandInfo HelpCommand { get; private set; }

      public IReadOnlyCollection<ParameterInfo> Properties => properties;

      #endregion

      #region Public Methods and Operators

      public static ArgumentClassInfo FromType(Type argumentClassType)
      {
         return new ArgumentClassInfo(argumentClassType);
      }

      public static ArgumentClassInfo FromType<T>()
      {
         return FromType(typeof(T));
      }

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

      private static ParameterInfo CreateInfo(PropertyInfo propertyInfo, CommandLineAttribute[] attributes)
      {
         foreach (var attribute in attributes)
         {
            if (attribute is CommandAttribute commandAttribute)
               return new CommandInfo(propertyInfo, commandAttribute);

            if (attribute is ArgumentAttribute argumentAttribute)
               return new ArgumentInfo(propertyInfo, argumentAttribute);

            if (attribute is OptionAttribute optionAttribute)
               return new OptionInfo(propertyInfo, optionAttribute);
         }

         return null;
      }

      private void Initialize()
      {
         commandInfos = new List<CommandInfo>();
         properties = new List<ParameterInfo>();

         foreach (var propertyInfo in ArgumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
         {
            var attributes = propertyInfo.GetCustomAttributes<CommandLineAttribute>(true).ToArray();
            if (attributes.Any())
            {
               var parameterInfo = CreateInfo(propertyInfo, attributes);
               properties.Add(parameterInfo);

               if (parameterInfo is CommandInfo commandInfo)
               {
                  commandInfos.Add(commandInfo);
                  if (IsHelpCommand(propertyInfo))
                     HelpCommand = commandInfo;
                  if (commandInfo.IsDefault)
                  {
                     if (DefaultCommand != null)
                        throw new InvalidOperationException("Default command was defined twice.");

                     DefaultCommand = commandInfo;
                  }
               }
            }
         }
      }

      private bool IsHelpCommand(PropertyInfo propertyInfo)
      {
         return propertyInfo.PropertyType == typeof(HelpCommand);
      }

      #endregion
   }
}