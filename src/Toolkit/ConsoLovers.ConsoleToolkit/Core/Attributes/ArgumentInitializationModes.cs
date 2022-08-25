// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentInitializationModes.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   public enum ArgumentInitializationModes
   {
      /// <summary>Behaviour is specified by the default behaviour of the <see cref="ICommandMenuManager"/></summary>
      Default,

      /// <summary>The arguments will be displayed as menu and can be changed there</summary>
      AsMenu,

      /// <summary>Before the command is executed, the arguments are requested from the user</summary>
      WhileExecution,

      /// <summary>The arguments are not initialized at all</summary>
      Custom,

   }
}