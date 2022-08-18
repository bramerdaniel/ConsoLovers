// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   /// <summary>Base class for a command that does not required any arguments</summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.CommandLineArguments.ICommand"/>
   public class Command : ICommand
   { 
      #region ICommand Members

      public virtual void Execute()
      {
      }

      #endregion
   }
}