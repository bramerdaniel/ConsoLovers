// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Reflection;

   using JetBrains.Annotations;

   [DebuggerDisplay("{" + nameof(GetDebuggerString) + "()}")]
   internal class MappingInfo
   {
      #region Constants and Fields

      private readonly MappingList mappingList;

      #endregion

      #region Constructors and Destructors

      internal MappingInfo([NotNull] PropertyInfo propertyInfo, [NotNull] CommandLineAttribute commandLineAttribute, [NotNull] MappingList mappingList)
      {
         this.mappingList = mappingList ?? throw new ArgumentNullException(nameof(mappingList));
         PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
         CommandLineAttribute = commandLineAttribute ?? throw new ArgumentNullException(nameof(commandLineAttribute));
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the command line argument.</summary>
      public CommandLineArgument CommandLineArgument { get; set; }

      /// <summary>Gets the command line attribute.</summary>
      public CommandLineAttribute CommandLineAttribute { get; }

      /// <summary>Gets a value indicating whether an index was specified on argument.</summary>
      public bool HasIndex => CommandLineAttribute.GetIndex() >= 0;
      
      public int Index => CommandLineAttribute.GetIndex();

      public string Name => CommandLineAttribute.Name ?? PropertyInfo.Name;

      /// <summary>Gets the property information.</summary>
      public PropertyInfo PropertyInfo { get; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Gets a name match of the given name.</summary>
      /// <param name="name">The name to get the <see cref="MappingInfo"/> for.</param>
      /// <returns>The found <see cref="MappingInfo"/> or null</returns>
      public MappingInfo GetNameMatch(string name)
      {
         return mappingList.GetMappingInfo(name);
      }

      /// <summary>Gets all names the mapping defines.</summary>
      /// <returns>An <see cref="IEnumerable{T}"/> of strings</returns>
      public IEnumerable<string> GetNames()
      {
         yield return Name;
         foreach (var aliase in CommandLineAttribute.Aliases)
            yield return aliase;
      }

      /// <summary>Determines whether the mapping is an option.</summary>
      /// <returns><c>true</c> if this instance is option; otherwise, <c>false</c>.</returns>
      public bool IsOption()
      {
         return CommandLineAttribute is OptionAttribute;
      }

      /// <summary>Determines whether the mapping is shared.</summary>
      /// <returns><c>true</c> if this instance is shared; otherwise, <c>false</c>.</returns>
      public bool IsShared()
      {
         return CommandLineAttribute.Shared;
      }

      #endregion

      #region Methods

      private string GetDebuggerString()
      {
         return $"{(IsOption() ? "Option" : "Argument")}: {Name}";
      }

      #endregion
   }
}