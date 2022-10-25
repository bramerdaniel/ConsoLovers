// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializationCancellationMode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   public enum InitializationCancellationMode
   {
      /// <summary>The command execution will be canceled</summary>
      Cancel,

      /// <summary>The command execution will be canceled and returned to the menu immediately</summary>
      CancelSilent,

      /// <summary>The command execution will continue with the default parameter value</summary>
      Continue
   }
}