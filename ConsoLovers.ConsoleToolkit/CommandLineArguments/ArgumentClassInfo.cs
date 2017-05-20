// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentClassInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   public class ArgumentClassInfo
   {
      #region Constants and Fields

      public bool? hasCommands;

      private List<CommandInfo> commandInfos;

      private List<ParameterInfo> properties;

      #endregion

      #region Constructors and Destructors

      public ArgumentClassInfo([NotNull] Type argumentType)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         ArgumentType = argumentType;
         Initialize();
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the type of the class containing the argument definitions.</summary>
      public Type ArgumentType { get; }

      public IReadOnlyCollection<CommandInfo> CommandInfos => commandInfos;

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

      private static ParameterInfo CreateInfo(PropertyInfo propertyInfo, CommandLineAttribute[] attributes)
      {
         foreach (var attribute in attributes)
         {
            var commandAttribute = attribute as CommandAttribute;
            if (commandAttribute != null)
               return new CommandInfo(propertyInfo, commandAttribute);

            var argumentAttribute = attribute as ArgumentAttribute;
            if (argumentAttribute != null)
               return new ArgumentInfo(propertyInfo, argumentAttribute);

            var optionAttribute = attribute as OptionAttribute;
            if (optionAttribute != null)
               return new OptionInfo(propertyInfo, optionAttribute);
         }

         return null;
      }

      private void Initialize()
      {
         commandInfos = new List<CommandInfo>();
         properties = new List<ParameterInfo>();

         foreach (var propertyInfo in ArgumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
         {
            var attributes = (CommandLineAttribute[])propertyInfo.GetCustomAttributes(typeof(CommandLineAttribute), true);
            if (attributes.Any())
            {
               var parameterInfo = CreateInfo(propertyInfo, attributes);
               properties.Add(parameterInfo);

               var commandInfo = parameterInfo as CommandInfo;
               if (commandInfo != null)
               {
                  commandInfos.Add(commandInfo);
                  if (IsHelpCommand(propertyInfo))
                     HelpCommand = commandInfo;
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