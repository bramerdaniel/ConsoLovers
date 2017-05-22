// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Reflection;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.Contracts;

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

      /// <summary>Gets the console that should be used.</summary>
      protected IConsole Console => console ?? (console = CreateConsole());

      protected virtual IConsole CreateConsole()
      {
         return  new ConsoleProxy();
      }

      #region Constructors and Destructors

      public PropertyHelpProvider([NotNull] PropertyInfo propertyInfo)
         : this(propertyInfo, null)
      {
      }

      public PropertyHelpProvider([NotNull] PropertyInfo propertyInfo, [CanBeNull] ResourceManager resourceManager)
      {
         if (propertyInfo == null)
            throw new ArgumentNullException(nameof(propertyInfo));

         this.propertyInfo = propertyInfo;
         this.resourceManager = resourceManager;

         commandLineAttribute = propertyInfo.GetAttribute<CommandLineAttribute>();
         helpTextAttribute = propertyInfo.GetAttribute<HelpTextAttribute>();
      }

      #endregion

      #region IHelpProvider Members

      public void PrintHelp()
      {
         WriteHeader();
         WriteContent();
         WriteFooter();
      }

      #endregion

      #region Public Methods and Operators

      public virtual void WriteContent()
      {
         if (helpTextAttribute != null)
            WriteHelpText(helpTextAttribute);
      }

      public virtual void WriteFooter()
      {
      }

      public virtual void WriteHeader()
      {
         Console.WriteLine($"Help for the {GetCommandLineTyp()} '{GetArgumentName()}'");
         Console.WriteLine();
      }

      #endregion

      #region Methods

      protected virtual void WriteHelpText(HelpTextAttribute helpText)
      {
         var helpTextString = resourceManager?.GetString(helpText.ResourceKey) ?? helpText.Description;
         Console.WriteLine($"- {helpTextString}");
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