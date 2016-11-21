// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;

   public abstract class ConsoleApplication
   {
      public static IConsole Console => ColoredConsole.Instance;
   }
}