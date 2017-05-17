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

      private readonly Type argumentType;

      private List<CommandArgumentInfo> commandProperties;

      private List<ArgumentInfo> properties;

      #endregion

      #region Constructors and Destructors

      public ArgumentClassInfo([NotNull] Type argumentType)
      {
         if (argumentType == null)
            throw new ArgumentNullException(nameof(argumentType));

         this.argumentType = argumentType;
         Initialize();
      }

      #endregion

      #region Public Properties

      public IReadOnlyCollection<CommandArgumentInfo> CommandProperties => commandProperties;

      public bool HasCommands => CommandProperties.Any();

      public IReadOnlyCollection<ArgumentInfo> Properties => properties;

      #endregion

      #region Methods

      private void Initialize()
      {
         commandProperties = new List<CommandArgumentInfo>();
         properties = new List<ArgumentInfo>();

         foreach (var propertyInfo in argumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
         {
            var attributes = propertyInfo.GetCustomAttributes(typeof(CommandAttribute), true);
            if (attributes.Any())
               properties.Add(new ArgumentInfo { PropertyInfo = propertyInfo });

            var commandAttributes = attributes.OfType<CommandAttribute>().ToArray();
            if (commandAttributes.Any())
               commandProperties.Add(new CommandArgumentInfo { PropertyInfo = propertyInfo, CommandAttribute = commandAttributes.FirstOrDefault() });
         }
      }

      #endregion
   }

   public class CommandArgumentInfo : ArgumentInfo
   {
      public CommandAttribute CommandAttribute { get; set; }
   }

   public class ArgumentInfo
   {
      #region Public Properties
      public PropertyInfo PropertyInfo { get; set; }


      #endregion
   }
}