// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelpProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using ConsoLovers.ConsoleToolkit.Core.DIContainer;
    using System;
    using System.Reflection;
    using System.Resources;

    /// <summary><see cref="IHelpProvider"/> implementation for properties</summary>
    /// <seealso cref="IHelpProvider"/>
    internal class PropertyHelpProvider : IHelpProvider
    {
        #region Private Fields

        private CommandLineAttribute commandLineAttribute;

        private IConsole console;

        private DetailedHelpTextAttribute detailedHelpTextAttribute;

        private HelpTextAttribute helpTextAttribute;

        private PropertyInfo propertyInfo;

        #endregion Private Fields

        #region Private Methods

        private string GetArgumentName()
        {
            string primaryName = propertyInfo.Name;

            if (commandLineAttribute?.Name != null)
                return commandLineAttribute.Name.ToLowerInvariant();

            return primaryName.ToLowerInvariant();
        }

        private string GetCommandLineTyp()
        {
            if (commandLineAttribute is OptionAttribute)
                return "option";
            if (commandLineAttribute is ArgumentAttribute)
                return "argument";
            if (commandLineAttribute is CommandAttribute)
                return "command";
            return "property";
        }

        #endregion Private Methods

        #region Protected Properties

        /// <summary>Gets the console that should be used.</summary>
        protected IConsole Console => console ?? (console = CreateConsole());

        #endregion Protected Properties

        #region Protected Methods

        protected virtual IConsole CreateConsole()
        {
            return new ConsoleProxy();
        }

        protected virtual void WriteHelpText(string resourceKey, string description)
        {
            if (ResourceManager != null && !string.IsNullOrEmpty(resourceKey))
            {
                var helpTextString = CommandLineEngine.GetLocalizedDescription(ResourceManager, resourceKey);
                Console.WriteLine($"- {helpTextString}");
            }
            else
            {
                if (description != null)
                    Console.WriteLine($"- {description}");
            }
        }

        protected virtual void WriteNoHelpTextAvailable()
        {
        }

        #endregion Protected Methods

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyHelpProvider" /> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        [InjectionConstructor]
        public PropertyHelpProvider(ResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
        }

        /// <summary>Initializes a new instance of the <see cref="PropertyHelpProvider"/> class.</summary>
        public PropertyHelpProvider()
           : this(null)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets or sets the resource manager.</summary>
        public ResourceManager ResourceManager { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>Prints the help.</summary>
        public void PrintHelp()
        {
            WriteHeader();
            WriteContent();
            WriteFooter();
        }

        public virtual void PrintPropertyHelp(PropertyInfo property)
        {
            propertyInfo = property;
            commandLineAttribute = propertyInfo.GetAttribute<CommandLineAttribute>();
            detailedHelpTextAttribute = propertyInfo.GetAttribute<DetailedHelpTextAttribute>();
            helpTextAttribute = propertyInfo.GetAttribute<HelpTextAttribute>();

            WriteHeader();
            WriteContent();
            WriteFooter();
        }

        public virtual void PrintTypeHelp(Type type)
        {
            throw new NotSupportedException();
        }

        /// <summary>Writes the content.</summary>
        public virtual void WriteContent()
        {
            if (detailedHelpTextAttribute != null)
            {
                WriteHelpText(detailedHelpTextAttribute.ResourceKey, detailedHelpTextAttribute.Description);
            }
            else if (helpTextAttribute != null)
            {
                WriteHelpText(helpTextAttribute.ResourceKey, helpTextAttribute.Description);
            }
            else
            {
                WriteNoHelpTextAvailable();
            }
        }

        /// <summary>Writes the footer.</summary>
        public virtual void WriteFooter()
        {
        }

        /// <summary>Writes the header.</summary>
        public virtual void WriteHeader()
        {
            Console.WriteLine($"Help for the {GetCommandLineTyp()} '{GetArgumentName()}'");
            Console.WriteLine();
        }

        #endregion Public Methods
    }
}