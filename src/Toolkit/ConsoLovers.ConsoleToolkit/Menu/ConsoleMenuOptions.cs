// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuOptions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.ComponentModel;
   using System.Runtime.CompilerServices;

   using ConsoLovers.ConsoleToolkit.Contracts;

   using JetBrains.Annotations;

   /// <summary>The options for a <see cref="ConsoleMenu"/></summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Menu.IConsoleMenuOptions" />
   public class ConsoleMenuOptions : IConsoleMenuOptions
   {
      #region Constants and Fields

      private bool circularSelection = true;

      private bool clearOnExecution = true;

      private bool executeOnIndexSelection;

      private ExpanderDescription expander = new ExpanderDescription();

      private int indentSize = 2;

      private bool indexMenuItems = true;

      private SelectionMode selectionMode = SelectionMode.UnifiedLength;

      private string selector = ">> ";

      #endregion

      #region Public Events

      public event PropertyChangedEventHandler PropertyChanged;

      #endregion

      #region IConsoleMenuOptions Members

      public bool CircularSelection
      {
         get => circularSelection;
         set
         {
            if (value == circularSelection)
               return;

            circularSelection = value;
            RaisePropertyChanged();
         }
      }

      public bool ClearOnExecution
      {
         get => clearOnExecution;
         set
         {
            if (value == clearOnExecution)
               return;
            clearOnExecution = value;
            RaisePropertyChanged();
         }
      }

      public bool ExecuteOnIndexSelection
      {
         get => executeOnIndexSelection;
         set
         {
            if (value == executeOnIndexSelection)
               return;
            executeOnIndexSelection = value;
            RaisePropertyChanged();
         }
      }

      public ExpanderDescription Expander
      {
         get => expander;
         set
         {
            if (value == null)
               throw new ArgumentNullException(nameof(value));

            if (Equals(value, expander))
               return;

            expander = value;
            RaisePropertyChanged();
         }
      }

      public int IndentSize
      {
         get => indentSize;
         set
         {
            if (value == indentSize)
               return;
            indentSize = value;
            RaisePropertyChanged();
         }
      }

      public bool IndexMenuItems
      {
         get => indexMenuItems;
         set
         {
            if (value == indexMenuItems)
               return;
            indexMenuItems = value;
            RaisePropertyChanged();
         }
      }

      public SelectionMode SelectionMode
      {
         get => selectionMode;
         set
         {
            if (value == selectionMode)
               return;
            selectionMode = value;
            RaisePropertyChanged();
         }
      }

      public string Selector
      {
         get => selector;
         set
         {
            if (value == selector)
               return;
            selector = value;
            RaisePropertyChanged();
         }
      }

      public object Footer { get; set; }

      public object Header { get; set; }

      public ConsoleKey[] CloseKeys { get; set; } = Array.Empty<ConsoleKey>();

      #endregion

      #region Methods

      [NotifyPropertyChangedInvocator]
      protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion
   }
}