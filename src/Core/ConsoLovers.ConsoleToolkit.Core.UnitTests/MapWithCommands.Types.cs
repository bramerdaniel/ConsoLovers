// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapWithCommands.Types.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System.IO;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

using JetBrains.Annotations;

public partial class MapWithCommands
{

   internal class ApplicationCommands
   {
      #region Public Properties

      [Command("Execute")]
      public Command Execute { get; set; }

      [Option("Help")]
      public bool Help { get; set; }

      #endregion
   }

   internal class ApplicationCommandsWithDefault
   {
      #region Public Properties

      [Command("ExecuteAsync", IsDefaultCommand = true)]
      public Command Execute { get; set; }

      [Command("ExecuteMany")]
      public Command ExecuteMany { get; set; }

      #endregion
   }

   internal class CommandWithPrivateSetter
   {
      #region Public Properties

      [Command("Execute", "e")]
      public Command Execute { get; [UsedImplicitly] private set; }

      #endregion
   }

   internal class ImutableCommands
   {
      #region Public Properties

      [Command("ExecuteAsync", "e")]
      public Command Execute { get; [UsedImplicitly] private set; }

      #endregion
   }

   internal class InvalidCommands
   {
      #region Public Properties

      [Command("Execute", "e")]
      public Command Execute => null;

      [Command("NoCommand", "nc")]
      public FileInfo NoCommand { get; set; }

      #endregion
   }
}