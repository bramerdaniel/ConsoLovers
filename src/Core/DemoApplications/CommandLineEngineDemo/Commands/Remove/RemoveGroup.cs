// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveGroup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo.Commands.Remove
{
   using ConsoLovers.ConsoleToolkit.Core;

   internal class RemoveGroup
   {
      [Command("simple")]
      [HelpText("Simple command without arguments")]
      public SimpleCommand Simple { get; set; }

      [Command("complex")]
      [HelpText("Complex command with arguments")]
      public ComplexCommand Complex { get; set; }
   }
}