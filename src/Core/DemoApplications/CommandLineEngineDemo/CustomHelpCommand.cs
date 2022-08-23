﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomHelpCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;
   using System.Reflection;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   [HelpTextProvider(typeof(CustomHelpCommand))]
   internal class CustomHelpCommand : ICommand, IHelpProvider
   {
      #region ICommand Members

      public void Execute()
      {
         Console.WriteLine($"CommandName = CustomHelpCommand");
         Console.ReadLine();
      }

      #endregion

      #region IHelpTextProvider Members


      private void PrintHelp()
      {
         WriteHeader();
         WriteContent();
         WriteFooter();
      }

      public void WriteContent()
      {
         Console.WriteLine(@"This is the custom help, that was created by a ICommand that implements provides an IHelpTextProvider");
      }

      public void WriteFooter()
      {
         Console.WriteLine(@"-------------------------------------------------------------------------------");
      }

      public void WriteHeader()
      {
         Console.WriteLine(@" _    _      _      ");
         Console.WriteLine(@"| |  | |    | |     ");
         Console.WriteLine(@"| |__| | ___| |_ __ ");
         Console.WriteLine(@"|  __  |/ _ \ | '_ \");
         Console.WriteLine(@"| |  | |  __/ | |_) |");
         Console.WriteLine(@"|_|  |_|\___|_| .__/");
         Console.WriteLine(@"              |_|   ");
      }

      #endregion

      public void PrintTypeHelp(Type type)
      {
         PrintHelp();
      }

      public void PrintPropertyHelp(PropertyInfo property)
      {
         PrintHelp();
      }
   }
}