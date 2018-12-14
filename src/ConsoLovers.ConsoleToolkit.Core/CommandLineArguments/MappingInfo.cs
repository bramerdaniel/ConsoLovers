// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Reflection;

   using JetBrains.Annotations;

   [DebuggerDisplay("{GetDebuggerString()}")]
   internal class MappingInfo
   {
      private readonly MappingList mappingList;

      internal MappingInfo([NotNull] PropertyInfo propertyInfo, [NotNull] CommandLineAttribute commandLineAttribute, [NotNull] MappingList mappingList)
      {
         this.mappingList = mappingList ?? throw new ArgumentNullException(nameof(mappingList));
         PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
         CommandLineAttribute = commandLineAttribute ?? throw new ArgumentNullException(nameof(commandLineAttribute));
      }

      #region Public Properties

      public CommandLineAttribute CommandLineAttribute { get;  }

      public CommandLineArgument CommandLineArgument { get; set; }

      public PropertyInfo PropertyInfo { get;  }

      public string Name => CommandLineAttribute.Name ?? PropertyInfo.Name;

      #endregion

      string GetDebuggerString()
      {
         return $"{(IsOption() ? "Option" : "Argument")}: {Name}";
      }

      #region Public Methods and Operators

      public bool IsOption()
      {
         return CommandLineAttribute is OptionAttribute;
      }

      #endregion

      public IEnumerable<string> GetNames()
      {
         yield return Name;
         foreach (var aliase in CommandLineAttribute.Aliases)
            yield return aliase;
      }

      public MappingInfo GetNameMatch(string name)
      {
         return mappingList.GetMappingInfo(name);
      }

      public bool IsShared()
      {
         return CommandLineAttribute.Shared;
      }
   }
}