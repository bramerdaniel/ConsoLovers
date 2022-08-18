// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaygroundApp.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class PlaygroundApp : ConsoleApplication<ApplicationArgs>
{
   #region Constructors and Destructors

   public PlaygroundApp(ICommandLineEngine commandLineEngine)
      : base(commandLineEngine)
   {
   }

   #endregion

   #region Methods

   protected override void OnUnhandledCommandLineArgument(object sender, CommandLineArgumentEventArgs e)
   {
      Console.WriteLine($"{e.Argument.Name} was not handled", ConsoleColor.DarkMagenta);
      base.OnUnhandledCommandLineArgument(sender, e);
   }

   #endregion
}