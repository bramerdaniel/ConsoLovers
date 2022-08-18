﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteRoleArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.Role;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class DeleteRoleArgs : ICustomizedFooter
{
   [Argument("name", Index = 0)]
   [HelpText("Name of the role to delete")]
   public string RoleName { get; set; } = null!;

   public void WriteFooter(IConsole console)
   {
      console.WriteLine();
      console.WriteLine();
      console.WriteLine("Usage:");
      console.WriteLine("  delete role <name>:");
      console.WriteLine();
   }
}