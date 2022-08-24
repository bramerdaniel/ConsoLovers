// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace InputBoxDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.Input;

   class Program
   {
      #region Methods

      private static string GetDefaultValue()
      {
         var colorNames = Enum.GetNames(typeof(ConsoleColor));
         var random = new Random(DateTime.Now.Millisecond);
         var background = random.Next(0, colorNames.Length - 1);
         var foreground = random.Next(0, colorNames.Length - 1);

         return colorNames[background] + " " + colorNames[foreground];
      }

      static void Main()
      {
         try
         {
            ShowInputBoxes();
         }
         catch (InputCanceledException e)
         {
            Console.WriteLine($"Input was cancelled with last value {e.CurrentInput}");
            Console.ReadLine();
         }
      }

      private static void ShowInputBoxes()
      {
         var password = new InputBox<string>("Enter password : ") { IsPassword = true }.ReadLine();
         Console.WriteLine(password);
         password = new InputBox<string>("Enter password : ", "Password") { IsPassword = true }.ReadLine(10);
         Console.WriteLine(password);
         password = new InputBox<string>("Enter password : ", "Password") { IsPassword = true, PasswordChar = '$', PlaceholderChar = '.' }.ReadLine(10);
         Console.WriteLine(password);

         int intValue = new InputBox<int>("Enter an integer: ")
         {
            CancellationHandler = int.Parse
         }
         .ReadLine();
         
         Console.WriteLine(intValue);

         double doubleValue = new InputBox<double>("Enter an double: ").ReadLine();
         Console.WriteLine(doubleValue);

         bool boolValue = new InputBox<bool>("Enter an bool : ").ReadLine();
         Console.WriteLine(boolValue);

         var text = new InputBox<string>("Enter some long text: ").ReadLine();
         Console.WriteLine(text);

         ConsoleColor background = ConsoleColor.White;
         ConsoleColor foreground = ConsoleColor.Red;

         while (true)
         {
            var inputBox = new InputBox<string>
            {
               Label = new InputLabel("Enter some text: ") { Foreground = ConsoleColor.Blue, Background = ConsoleColor.Gray },
               InitialValue = GetDefaultValue(),
               Foreground = foreground,
               Background = background
            };

            var input = inputBox.ReadLine(26);
            var colors = input.Split(' ');

            if (Enum.TryParse(colors[0], out background))
            {
               if (colors.Length > 1)
                  Enum.TryParse(colors[1], out foreground);

               continue;
            }

            if (input == "exit")
               return;

            Console.WriteLine(input);
         }
      }

      #endregion
   }
}