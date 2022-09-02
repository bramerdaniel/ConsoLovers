// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelpProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   /// <summary><see cref="IHelpProvider"/> implementation for properties</summary>
   /// <seealso cref="IHelpProvider"/>
   internal class PropertyHelpProvider : IHelpProvider
   {
      private readonly ILocalizationService localizationService;

      #region Constants and Fields

      private CommandLineAttribute commandLineAttribute;

      private IConsole console;

      private DetailedHelpTextAttribute detailedHelpTextAttribute;

      private HelpTextAttribute helpTextAttribute;

      private PropertyInfo propertyInfo;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="PropertyHelpProvider" /> class.</summary>
      /// <param name="localizationService"></param>
      [InjectionConstructor]
      public PropertyHelpProvider(ILocalizationService localizationService)
      {
         this.localizationService = localizationService;
      }

      /// <summary>Initializes a new instance of the <see cref="PropertyHelpProvider"/> class.</summary>
      public PropertyHelpProvider()
         : this(null)
      {
      }

      #endregion

      #region IHelpProvider Members

      public virtual void PrintTypeHelp(Type type)
      {
         throw new NotSupportedException();
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

      #endregion

      #region Properties

      /// <summary>Gets the console that should be used.</summary>
      protected IConsole Console => console ??= CreateConsole();

      #endregion

      #region Public Methods and Operators

      /// <summary>Prints the help.</summary>
      public void PrintHelp()
      {
         WriteHeader();
         WriteContent();
         WriteFooter();
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

      #endregion

      #region Methods

      protected virtual IConsole CreateConsole()
      {
         return new ConsoleProxy();
      }

      protected virtual void WriteHelpText(string resourceKey, string description)
      {
         if (localizationService != null && !string.IsNullOrEmpty(resourceKey))
         {
            var helpTextString = localizationService.GetLocalizedSting(resourceKey);
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