﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuilderOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   internal class MenuBuilder
   {
      #region Constructors and Destructors

      public MenuBuilder([NotNull] IMenuBuilderOptions options)
      {
         Options = options ?? throw new ArgumentNullException(nameof(options));
      }

      #endregion

      #region Properties

      internal IMenuBuilderOptions Options { get; }

      #endregion

      #region Methods

      internal IEnumerable<IMenuNode> Build<T>()
      {
         return Build(typeof(T));
      }

      private IEnumerable<IMenuNode> Build(Type argumentType)
      {
         var itemInfos = NodeInfo.CreateMenuInfos(argumentType, Options);
         return CreateMenuNodes(itemInfos);
      }

      private IEnumerable<IMenuNode> CreateMenuNodes(IEnumerable<NodeInfo> itemInfos)
      {
         var nodeInfos = itemInfos.OrderBy(x => x.DisplayOrder)
            .ToArray();

         foreach (var itemInfo in nodeInfos)
         {
            var menuNode = CreateMenuItemNode(itemInfo);
            if (menuNode != null)
               yield return menuNode;
         }
      }

      private IMenuNode CreateMenuItemNode(NodeInfo itemInfo)
      {
         if (itemInfo.IsCommand())
         {
            return new CommandNode
            {
               PropertyInfo = itemInfo.PropertyInfo,
               DisplayName = itemInfo.DisplayName,
               DisplayOrder = itemInfo.DisplayOrder,
               Type = itemInfo.PropertyInfo.PropertyType,
               InitializationMode = itemInfo.InitializationMode,
               IsVisible = ComputeIsVisible(itemInfo),
               ArgumentType = itemInfo.ArgumentType,
               Nodes = CreateMenuNodes(itemInfo.ChildInfos).ToArray()
            };
         }

         return new ArgumentNode
         {
            PropertyInfo = itemInfo.PropertyInfo,
            DisplayName = itemInfo.DisplayName,
            DisplayOrder = itemInfo.DisplayOrder,
            IsPassword = itemInfo.IsPassword,
            Required = itemInfo.IsRequired,
            Type = itemInfo.PropertyInfo.PropertyType,
            ShowInMenu = ComputeShowInMenu(itemInfo),
            ShowInInitialization = ComputeShowInInitialization(itemInfo)
         };
      }

      private bool ComputeIsVisible(NodeInfo itemInfo)
      {
         if (itemInfo.MenuCommandAttribute != null)
         {
            if (itemInfo.MenuCommandAttribute.Visibility == CommandVisibility.Visible)
               return true;
            if (itemInfo.MenuCommandAttribute.Visibility == CommandVisibility.Hidden)
               return false;

            return true;
         }

         if (Options.MenuBehaviour == MenuBuilderBehaviour.ShowAllCommand)
            return true;
         return false;
      }

      private bool ComputeShowInMenu(NodeInfo itemInfo)
      {
         if (itemInfo.MenuArgumentAttribute != null)
         {
            if (itemInfo.MenuArgumentAttribute.Visibility.HasFlag(ArgumentVisibility.InMenu))
               return true;

            if (itemInfo.MenuArgumentAttribute.Visibility == ArgumentVisibility.NotSpecified)
               return true;

            return false;
         }

         return true;
      }

      private bool ComputeShowInInitialization(NodeInfo itemInfo)
      {
         if (itemInfo.MenuArgumentAttribute != null)
         {
            if (itemInfo.MenuArgumentAttribute.Visibility.HasFlag(ArgumentVisibility.InInitialization))
               return true;

            if (itemInfo.MenuArgumentAttribute.Visibility == ArgumentVisibility.NotSpecified)
               return true;

            return false;
         }

         return true;
      }

      #endregion

      [DebuggerDisplay("{DisplayName}")]
      private class NodeInfo
      {
         #region Constants and Fields

         private readonly IMenuBuilderOptions options;

         #endregion

         #region Constructors and Destructors

         public NodeInfo([NotNull] PropertyInfo propertyInfo, [NotNull] IMenuBuilderOptions options)
         {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
         }

         #endregion

         #region Public Properties

         public ArgumentAttribute ArgumentAttribute { get; private set; }

         public Type ArgumentType { get; private set; }

         public NodeInfo[] ChildInfos { get; private set; }

         public CommandAttribute CommandAttribute { get; set; }

         public string DisplayName => ComputeDisplayName();

         public int DisplayOrder => ComputeDisplayOrder();

         public ArgumentInitializationModes InitializationMode { get; private set; }

         public MenuCommandAttribute MenuCommandAttribute { get; set; }

         public MenuArgumentAttribute MenuArgumentAttribute { get; set; }

         public PropertyInfo PropertyInfo { get; }

         public bool IsVisible { get; private set; }

         public bool IsPassword { get; private set; }

         public bool IsRequired { get; private set; }

         #endregion

         #region Public Methods and Operators

         public NodeInfo Initialize()
         {
            ArgumentType = ComputeArgumentType();
            InitializationMode = ComputeInitializationMode();
            IsVisible = ComputeIsVisible();
            ChildInfos = CreateChildren().ToArray();
            IsPassword = MenuArgumentAttribute?.IsPassword ?? false;
            IsRequired = ArgumentAttribute?.Required ?? false;

            return this;
         }

         private bool ComputeIsVisible()
         {
            // TODO 
            if (MenuCommandAttribute != null)
               return false;

            if (IsArgument())
               return ComputeArgumentVisibility();

            // TODO ComputeIsVisible
            return true;
         }

         private bool ComputeArgumentVisibility()
         {
            if (InitializationMode == ArgumentInitializationModes.AsMenu)
               return true;
            return false;
         }

         public bool IsCommand()
         {
            return CommandAttribute != null || MenuCommandAttribute != null;
         }

         #endregion

         #region Methods

         internal static IEnumerable<NodeInfo> CreateMenuInfos(Type argumentType, IMenuBuilderOptions options)
         {
            foreach (var property in argumentType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
               var menuInfo = new NodeInfo(property, options);

               var customAttributes = property.GetCustomAttributes();
               foreach (var customAttribute in customAttributes)
               {
                  if (customAttribute is CommandAttribute command)
                     menuInfo.CommandAttribute = command;

                  if (customAttribute is MenuCommandAttribute menuAttribute)
                     menuInfo.MenuCommandAttribute = menuAttribute;

                  if (customAttribute is MenuArgumentAttribute menuArgument)
                     menuInfo.MenuArgumentAttribute = menuArgument;

                  if (customAttribute is ArgumentAttribute argumentAttribute)
                     menuInfo.ArgumentAttribute = argumentAttribute;
               }

               if (menuInfo.IsValid())
                  yield return menuInfo.Initialize();
            }
         }



         private bool IsValid()
         {
            return IsCommand() || IsArgument();
         }

         private bool IsArgument()
         {
            return ArgumentAttribute != null || MenuArgumentAttribute != null;
         }

         private Type ComputeArgumentType()
         {
            var command = PropertyInfo.PropertyType.GetInterface(nameof(ICommandBase));
            if (command != null && PropertyInfo.PropertyType.GenericTypeArguments.Length == 1)
               return PropertyInfo.PropertyType.GenericTypeArguments[0];

            var argumentProperty = PropertyInfo.PropertyType.GetProperty(nameof(ICommandArguments<Type>.Arguments));
            if (argumentProperty != null)
               return argumentProperty.PropertyType;

            return null;
         }

         private string ComputeDisplayName()
         {
            var displayName = MenuCommandAttribute?.DisplayName;
            if (!string.IsNullOrWhiteSpace(displayName))
               return displayName;

            displayName = MenuArgumentAttribute?.DisplayName;
            if (!string.IsNullOrWhiteSpace(displayName))
               return displayName;

            displayName = ArgumentAttribute?.Name;
            if (!string.IsNullOrWhiteSpace(displayName))
               return displayName;

            return CommandAttribute?.Name ?? PropertyInfo.Name;
         }

         private int ComputeDisplayOrder()
         {
            if (MenuCommandAttribute != null)
               return MenuCommandAttribute.DisplayOrder;

            if (MenuArgumentAttribute != null)
               return MenuArgumentAttribute.DisplayOrder;

            return int.MaxValue;
         }

         private ArgumentInitializationModes ComputeInitializationMode()
         {
            var initMode = MenuCommandAttribute?.ArgumentInitialization ?? ArgumentInitializationModes.NotSpecified;
            if (initMode == ArgumentInitializationModes.NotSpecified)
               return options.ArgumentInitializationMode;
            return initMode;
         }

         private IEnumerable<NodeInfo> CreateChildren()
         {
            if (ArgumentType != null)
               return CreateMenuInfos(ArgumentType, options);

            return Enumerable.Empty<NodeInfo>();
         }

         #endregion
      }
   }
}