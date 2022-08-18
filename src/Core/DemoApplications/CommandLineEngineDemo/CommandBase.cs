namespace CommandLineEngineDemo
{
   using System;
   using System.Collections.Generic;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class CommandBase<T> : ICommand<T>
   {
      protected IConsole Console { get; } = new ConsoleProxy();

      private void PrintArgs(T args)
      {
         if (args == null)
         {
            Console.WriteLine(" no arguments");
            return;
         }

         Console.WriteLine(" ### command arguments ###");
         ConsoleColor color = ConsoleColor.White;
         foreach (var propertyInfo in args.GetType().GetProperties())
         {
            var commandLineAttribute = propertyInfo.GetCustomAttribute<CommandLineAttribute>();
            var value = propertyInfo.GetValue(args);
            if (value != null)
            {
               color= color == ConsoleColor.White ? ConsoleColor.Gray : ConsoleColor.White;
               Console.WriteLine($"  - {propertyInfo.Name.PadRight(10)} = {value.ToString().PadRight(40)} [Shared={commandLineAttribute?.Shared}]", color);
            }
         }
      }

      public void Execute()
      {
         BeforeExecute();

         Console.WriteLine(GetType().Name);
         PrintArgs(Arguments);
         ExecuteOverride();

      }

      protected virtual void ExecuteOverride()
      {
      }
      protected virtual void BeforeExecute()
      {
      }

      public T Arguments { get; set; }
   }
}