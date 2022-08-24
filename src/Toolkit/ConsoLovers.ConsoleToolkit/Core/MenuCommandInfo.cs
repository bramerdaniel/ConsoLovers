// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuCommandInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   internal class MenuCommandInfo
   {
      public MenuCommandInfo([NotNull] CommandInfo commandInfo)
      {
         CommandInfo = commandInfo ?? throw new ArgumentNullException(nameof(commandInfo));
      }

      public string DisplayName { get; set; }

      public bool Visible { get; set; }

      /// <summary>Gets the <see cref="CommandInfo"/> this <see cref="MenuCommandInfo"/> was create for.</summary>
      public CommandInfo CommandInfo { get; }

      public ArgumentInitializationModes ArgumentInitializationMode { get; set; }

      public ArgumentClassInfo ArgumentInfo { get; set; }

      public IEnumerable<MenuArgumentInfo> GetArgumentInfos()
      {
         if (ArgumentInfo != null)
         {
            foreach (var property in ArgumentInfo.Properties)
            {
               var argumentAttribute = property.PropertyInfo.GetAttribute<MenuArgumentAttribute>();
               yield return new MenuArgumentInfo(property)
               {
                  DisplayName = argumentAttribute?.DisplayName ?? property.ParameterName 
               };
            }
         }
      }
   }
}