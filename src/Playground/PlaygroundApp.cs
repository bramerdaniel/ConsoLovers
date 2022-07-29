﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaygroundApp.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PlaygroundApp : ConsoleApplication<ApplicationArgs>
{
   public PlaygroundApp(ICommandLineEngine commandLineEngine)
      : base(commandLineEngine)
   {
   }

   protected override void OnUnhandledCommandLineArgument(object sender, CommandLineArgumentEventArgs e)
   {
      Console.WriteLine($"{e.Argument.Name} was not handled", ConsoleColor.DarkMagenta);
      base.OnUnhandledCommandLineArgument(sender, e);
   }
}