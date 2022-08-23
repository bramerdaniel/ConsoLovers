// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelpRequest.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Reflection;

   using JetBrains.Annotations;

   public class PropertyHelpRequest
   {
      #region Constructors and Destructors

      public PropertyHelpRequest([NotNull] PropertyInfo property)
      {
         Property = property ?? throw new ArgumentNullException(nameof(property));

         CommandLineAttribute = Property.GetAttribute<CommandLineAttribute>();
         DetailedHelpTextAttribute = Property.GetAttribute<DetailedHelpTextAttribute>();
         HelpTextAttribute = Property.GetAttribute<HelpTextAttribute>();
      }

      #endregion

      #region Public Properties

      public CommandLineAttribute CommandLineAttribute { get; }

      public DetailedHelpTextAttribute DetailedHelpTextAttribute { get; }

      public HelpTextAttribute HelpTextAttribute { get; }

      /// <summary>Gets the property.</summary>
      public PropertyInfo Property { get; }

      #endregion
   }
}