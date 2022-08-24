// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XCopyApplication.Commands
{
   using System.IO;

   using ConsoLovers.ConsoleToolkit.Core;

   public class CopyCommand : ICommand<CopyCommandArgs>
   {
      #region ICommand Members

      public void Execute()
      {
         File.Copy(Arguments.SourceFile, Arguments.DestinationFile, Arguments.OverwriteExisting);
      }

      #endregion

      #region ICommand<CopyCommandArgs> Members

      public CopyCommandArgs Arguments { get; set; }

      #endregion
   }

   public class MoveCommand : ICommand<CopyCommandArgs>
   {
      #region ICommand Members

      public void Execute()
      {
         File.Move(Arguments.SourceFile, Arguments.DestinationFile);
      }

      #endregion

      #region ICommand<CopyCommandArgs> Members

      public CopyCommandArgs Arguments { get; set; }

      #endregion
   }
}