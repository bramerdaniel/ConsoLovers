// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;

   internal class ExecuteCommand : CommandBase<ExecuteArgs>
   {
      #region ICommand Members

      protected override void ExecuteOverride()
      {
         if (Arguments.Wait)
            Console.ReadLine();
      }

      #endregion
   }
}