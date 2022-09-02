// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleInputReader.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.Input;
   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   internal class ConsoleInputReader : IInputReader
   {
      private readonly IConsole console;

      public ConsoleInputReader(IConsole console)
      {
         this.console = console;
      }

      public object ReadValue(IArgumentNode argumentNode, object initialValue)
      {
         if (!string.IsNullOrWhiteSpace(argumentNode.Description))
            console.WriteLine(argumentNode.Description);

         if (argumentNode.Type == typeof(int))
         {
            if (initialValue is int intValue)
               return new InputBox<int>($"{argumentNode.DisplayName}: ", intValue).ReadLine();
            return new InputBox<int>($"{argumentNode.DisplayName}: ").ReadLine();
         }

         if (argumentNode.Type == typeof(bool))
            return new InputBox<bool>($"{argumentNode.DisplayName}: ", initialValue is bool boolValue && boolValue).ReadLine();

         if (argumentNode.Type == typeof(string))
         {
            var stringValue = initialValue as string ?? string.Empty;
            return new InputBox<string>($"{argumentNode.DisplayName}: ", stringValue) { IsPassword = argumentNode.IsPassword }.ReadLine();
         }

         return new InputBox<object>($"{argumentNode.DisplayName}: ", initialValue) { IsPassword = argumentNode.IsPassword }.ReadLine();
      }
   }
}