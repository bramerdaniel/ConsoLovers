﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingList.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   internal class MappingList : List<MappingInfo>
   {
      #region Constants and Fields

      readonly Dictionary<string, MappingInfo> definedNames = new Dictionary<string, MappingInfo>();

      [NotNull]
      private readonly Type type;

      #endregion

      #region Constructors and Destructors

      public static MappingList FromType<T>()
      {
         return new MappingList(typeof(T));
      }

      private MappingList([NotNull] Type type)
      {
         this.type = type ?? throw new ArgumentNullException(nameof(type));

         foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
         {
            var commandLineAttribute = GetCommandLineAttribute(propertyInfo);
            if (commandLineAttribute != null)
            {
               var mappingInfo = new MappingInfo (propertyInfo,commandLineAttribute, this);
               EnsureUnique(mappingInfo);

               Add(mappingInfo);
            }
         }
      }

      #endregion

      #region Public Methods and Operators

      public MappingInfo GetMappingInfo(string name)
      {
         return definedNames.TryGetValue(name, out var mappingInfo) ? mappingInfo : null;
      }

      public bool TryGetMappingInfo(string name, out MappingInfo mappingInfo)
      {
         return definedNames.TryGetValue(name, out mappingInfo);
      }

      #endregion

      #region Methods

      internal void EnsureUnique(MappingInfo mappingInfo)
      {
         var namesToDefine = new List<string> { mappingInfo.CommandLineAttribute.Name ?? mappingInfo.PropertyInfo.Name };
         namesToDefine.AddRange(mappingInfo.CommandLineAttribute.Aliases);

         foreach (var name in namesToDefine)
         {
            if (definedNames.TryGetValue(name, out var existingMapping))
            {
               var message = $"The properties '{existingMapping.PropertyInfo.Name}' and '{mappingInfo.PropertyInfo.Name}' of the class '{mappingInfo.PropertyInfo.DeclaringType?.Name}' define both a name (or alias) called '{name}'";
               throw new CommandLineAttributeException(message) { Name = name, FirstProperty = existingMapping.PropertyInfo, SecondProperty = mappingInfo.PropertyInfo };
            }
         }

         foreach (var name in namesToDefine)
            definedNames[name] = mappingInfo;
      }

      private static CommandLineAttribute GetCommandLineAttribute(PropertyInfo propertyInfo)
      {
         return propertyInfo.GetCustomAttributes<CommandLineAttribute>(true).FirstOrDefault();
      }

      #endregion
   }
}