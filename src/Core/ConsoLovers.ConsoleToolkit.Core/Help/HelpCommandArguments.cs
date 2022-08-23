﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommandArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   /// <summary>Arguments class for the help command</summary>
   public class HelpCommandArguments
   {
      #region Public Properties

      /// <summary>Gets or sets the argument dictionary.</summary>
      public ICommandLineArguments ArgumentDictionary { get; set; }

      /// <summary>Gets or sets the argument infos.</summary>
      public ArgumentClassInfo ArgumentInfos { get; set; }

      #endregion
   }
}