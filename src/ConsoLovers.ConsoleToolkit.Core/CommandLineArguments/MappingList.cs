// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingList.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>Helper class that creates a list of <see cref="MappingInfo"/> for a specified type</summary>
    /// <seealso cref="System.Collections.Generic.List{MappingInfo}"/>
    internal class MappingList : List<MappingInfo>
    {
        #region Constants and Fields

        private readonly Dictionary<string, MappingInfo> definedNames = new Dictionary<string, MappingInfo>();

        [NotNull]
        private readonly Type type;

        #endregion Constants and Fields

        #region Constructors and Destructors

        private MappingList([NotNull] Type type)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
            Initialize();
        }

        #endregion Constructors and Destructors

        #region Public Methods and Operators

        public static MappingList FromType<T>()
        {
            return new MappingList(typeof(T));
        }

        public MappingInfo GetMappingInfo(string name)
        {
            return definedNames.TryGetValue(name, out var mappingInfo) ? mappingInfo : null;
        }

        public bool TryGetMappingInfo(string name, out MappingInfo mappingInfo)
        {
            return definedNames.TryGetValue(name, out mappingInfo);
        }

        #endregion Public Methods and Operators

        #region Methods

        private static CommandLineAttribute GetCommandLineAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes<CommandLineAttribute>(true).FirstOrDefault();
        }

        private void Initialize()
        {
            var indexedArgs = new List<MappingInfo>();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var commandLineAttribute = GetCommandLineAttribute(propertyInfo);
                if (commandLineAttribute != null)
                {
                    var mappingInfo = new MappingInfo(propertyInfo, commandLineAttribute, this);
                    EnsureUnique(mappingInfo);

                    if (mappingInfo.HasIndex)
                    {
                        indexedArgs.Add(mappingInfo);
                    }
                    else
                    {
                        // Options have to be mapped first to make sure that index based mapping of arguments still works later
                        Add(mappingInfo);
                    }
                }
            }

            foreach (var mappingInfo in indexedArgs.OrderBy(x => x.Index))
                Add(mappingInfo);
        }

        internal void EnsureUnique(MappingInfo mappingInfo)
        {
            var namesToDefine = new List<string> { mappingInfo.CommandLineAttribute.Name ?? mappingInfo.PropertyInfo.Name };
            namesToDefine.AddRange(mappingInfo.CommandLineAttribute.Aliases);

            foreach (var name in namesToDefine)
            {
                if (definedNames.TryGetValue(name, out var existingMapping))
                {
                    var message =
                       $"The properties '{existingMapping.PropertyInfo.Name}' and '{mappingInfo.PropertyInfo.Name}' of the class '{mappingInfo.PropertyInfo.DeclaringType?.Name}' define both a name (or alias) called '{name}'";
                    throw new CommandLineAttributeException(message) { Name = name, FirstProperty = existingMapping.PropertyInfo, SecondProperty = mappingInfo.PropertyInfo };
                }
            }

            foreach (var name in namesToDefine)
                definedNames[name] = mappingInfo;
        }

        #endregion Methods
    }
}