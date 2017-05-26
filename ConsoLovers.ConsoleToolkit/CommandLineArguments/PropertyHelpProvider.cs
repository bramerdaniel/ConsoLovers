// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelpProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Reflection;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   /// <summary><see cref="IHelpProvider"/> implementation for properties</summary>
   /// <seealso cref="IHelpProvider"/>
   public class PropertyHelpProvider : IHelpProvider
   {
      #region Constants and Fields

      private readonly CommandLineAttribute commandLineAttribute;

      private readonly HelpTextAttribute helpTextAttribute;

      [NotNull]
      private readonly PropertyInfo propertyInfo;

      private readonly ResourceManager resourceManager;

      private IConsole console;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="PropertyHelpProvider"/> class.</summary>
      /// <param name="propertyInfo">The property information.</param>
      public PropertyHelpProvider([NotNull] PropertyInfo propertyInfo)
         : this(propertyInfo, null)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="PropertyHelpProvider"/> class.</summary>
      /// <param name="propertyInfo">The property information.</param>
      /// <param name="resourceManager">The resource manager.</param>
      /// <exception cref="System.ArgumentNullException">propertyInfo</exception>
      [InjectionConstructor]
      public PropertyHelpProvider([NotNull] PropertyInfo propertyInfo, [CanBeNull] ResourceManager resourceManager)
      {
         this.propertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
         this.resourceManager = resourceManager;

         commandLineAttribute = propertyInfo.GetAttribute<CommandLineAttribute>();
         helpTextAttribute = propertyInfo.GetAttribute<HelpTextAttribute>();
      }

      #endregion

      #region IHelpProvider Members

      /// <summary>Prints the help.</summary>
      public void PrintHelp()
      {
         WriteHeader();
         WriteContent();
         WriteFooter();
      }

      #endregion

      #region Properties

      /// <summary>Gets the console that should be used.</summary>
      protected IConsole Console => console ?? (console = CreateConsole());

      #endregion

      #region Public Methods and Operators

      /// <summary>Writes the content.</summary>
      public virtual void WriteContent()
      {
         if (helpTextAttribute != null)
            WriteHelpText(helpTextAttribute);
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

      #endregion

      #region Methods

      protected virtual IConsole CreateConsole()
      {
         return new ConsoleProxy();
      }

      /// <summary>Writes the help text that was specified with a <see cref="HelpTextAttribute"/>.</summary>
      /// <param name="helpText">The help text.</param>
      protected virtual void WriteHelpText(HelpTextAttribute helpText)
      {
         if (resourceManager != null)
         {
            if (!string.IsNullOrEmpty(helpText.DetailedResourceKey))
            {
               var helpTextString = resourceManager.GetString(helpText.DetailedResourceKey);
               Console.WriteLine($"- {helpTextString}");
               return;
            }

            if (!string.IsNullOrEmpty(helpText.ResourceKey))
            {
               var helpTextString = resourceManager.GetString(helpText.ResourceKey);
               Console.WriteLine($"- {helpTextString}");
               return;
            }
         }

         if (!string.IsNullOrEmpty(helpText.DetailedDescription))
         {
            Console.WriteLine($"- {helpText.DetailedDescription}");
            return;
         }

         if (!string.IsNullOrEmpty(helpText.Description))
         {
            Console.WriteLine($"- {helpText.Description}");
         }
      }

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

      #endregion
   }
}