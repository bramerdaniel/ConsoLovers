// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentInitializationModes.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   /// <summary>Enum describing how the arguments of a command are initialized when the command is executed from the menu</summary>
   public enum ArgumentInitializationModes
   {
      /// <summary>Behaviour is specified by the default behaviour of the <see cref="IMenuCommandManager"/></summary>
      NotSpecified,

      /// <summary>The arguments will be displayed as menu and can be changed there</summary>
      AsMenu,

      /// <summary>Before the command is executed, the arguments are requested from the user</summary>
      WhileExecution,

      /// <summary>The command must implement the <see cref="IMenuArgumentInitializer"/> interface and perform a custom initialization</summary>
      Custom,

      /// <summary>The arguments are not initialized at all</summary>
      None
   }
}