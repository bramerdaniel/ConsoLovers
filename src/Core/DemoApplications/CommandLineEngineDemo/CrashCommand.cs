// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrashCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   [HelpTextProvider(typeof(CrashCommand))]
   internal class CrashCommand : ICommand, IHelpProvider
   {
      #region ICommand Members

      public void Execute()
      {
         throw new InvalidOperationException("The command crashed");
      }

      #endregion

      public void PrintTypeHelp(Type type)
      {
         new ConsoleProxy().WriteLine("This is the fancy help of a command without parameters.", ConsoleColor.Cyan);
      }

      public void PrintPropertyHelp(PropertyInfo property)
      {
         new ConsoleProxy().WriteLine("This is the fancy help of a command without parameters.", ConsoleColor.Cyan);
      }
   }
}