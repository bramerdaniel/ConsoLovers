// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteRoleArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.Role;

using ConsoLovers.ConsoleToolkit.Core;

public class DeleteRoleArgs : ICustomizedFooter, IArgumentSink
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

   public bool TakeArgument(CommandLineArgument argument)
   {
      if (Enum.TryParse<ConsoleColor>(argument.Name, out var value))
      {
         Console.ForegroundColor = value;
         return true;
      }

      return false;

   }
}