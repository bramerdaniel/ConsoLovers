// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaygroundApp.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PlaygroundApp : ConsoleApplication<PayArgs>
{
   public PlaygroundApp(ICommandLineEngine commandLineEngine)
      : base(commandLineEngine)
   {
   }
}